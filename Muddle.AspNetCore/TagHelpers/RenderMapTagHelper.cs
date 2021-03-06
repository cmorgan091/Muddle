﻿using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Muddle.Domain.Helpers;
using Muddle.Domain.Models;

namespace Muddle.AspNetCore.TagHelpers
{
    public class RenderMapTagHelper: TagHelper
    {
        public Map Map { get; set; }

        /// <summary>
        /// Include the numeric grid coordinates in the rendered map (helpful for debugging)
        /// </summary>
        public bool ShowCoordinates { get; set; }

        private string _rootImagePath = "/muddle/img/";
        private string _iconsetImagePath = "/muddle/img/iconsets/default/";//= "_content/Muddle/muddle/img/iconsets/default/";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var sb = new StringBuilder();

            sb.Append($@"<table class='muddle-map' id='muddle-map'>");

            if (ShowCoordinates)
            {
                sb.Append($@"
     <tr>
        <th class='m-0 p-0'>
            #
        </th>
");

                for (var x = Map.MinX; x <= Map.MaxX; x++)
                {
                    sb.Append($@"
        <th class='text-center'>
            {x}
        </th>
");
                }

                sb.Append($@"
    </tr>
");
            }

            for (var y = Map.MinY; y <= Map.MaxY; y++)
            {
                sb.Append($@"<tr>");

                if (ShowCoordinates)
                {
                    sb.Append($@"                
        <th class='align-middle'>
            {y}
        </th>
");
                }

                for (var x = Map.MinX; x <= Map.MaxX; x++)
                {
                    var point = Map.GetPoint(x, y);

                    if (point.IsShrouded)
                    {
                        sb.Append($"<td class='m-0 p-0 muddle-map-cell'><img src='{_rootImagePath}transparent.png' alt='Shroud' class='icon' style='background-color:#000000;'/></td>");

                        continue;
                    }

                    sb.Append("<td class='m-0 p-0 muddle-map-cell'>");

                    if (point.HasPath)
                    {
                        switch (point.PathOrientation)
                        {
                            case Orientations.Vertical:
                                sb.Append(point.PathTerminusDirection.HasValue
                                    ? $"<img src='{_iconsetImagePath}path-end-{point.PathTerminusDirection?.ToString().ToLower()}.png' alt='Path end' class='icon'/>"
                                    : $"<img src='{_iconsetImagePath}path-vertical.png' alt='Path' class='icon'/>");

                                break;

                            case Orientations.Horizontal:
                                sb.Append(point.PathTerminusDirection.HasValue
                                    ? $"<img src='{_iconsetImagePath}path-end-{point.PathTerminusDirection?.ToString().ToLower()}.png' alt='Path end' class='icon'/>"
                                    : $"<img src='{_iconsetImagePath}path-horizontal.png' alt='Path' class='icon'/>");
                                break;

                            case Orientations.Both:
                                var junction = point.GetJunction();

                                switch (junction.Type)
                                {
                                    case Junction.JunctionTypes.Crossroad:
                                        sb.Append($"<img src='{_iconsetImagePath}path-junction-crossroad.png' alt='Crossroad' class='icon'/>");
                                        break;
                                    case Junction.JunctionTypes.TJunction:
                                        sb.Append($"<img src='{_iconsetImagePath}path-junction-tjunction-{junction.FromDirection.ToString().ToLower()}.png' alt='T Junction' class='icon'/>");
                                        break;
                                    case Junction.JunctionTypes.Righthand:
                                        sb.Append($"<img src='{_iconsetImagePath}path-junction-righthand-{junction.FromDirection.ToString().ToLower()}.png' alt='Corner' class='icon'/>");
                                        break;
                                    default:
                                        throw new Exception($"Unknown junction type {junction.Type}");
                                }

                                break;

                            default:
                                sb.Append(point.PathOrientation.CharRepresentation());
                                break;
                        }

                    }
                    else
                    {
                        if (point.BackgroundItems.Any())
                        {
                            var backgroundItem = point.BackgroundItems.Single();
                            sb.Append($"<img src='{_iconsetImagePath}backgrounditem-{backgroundItem.Width}x{backgroundItem.Height}-{backgroundItem.TileNumber}.png' alt='Background item' class='icon'/>");
                        }
                        else
                        {
                            sb.Append($"<img src='{_iconsetImagePath}default.png' alt='Default background' class='icon'/>");
                        }
                    }

                    if (point.PointOfInterests.Any())
                    {
                        var poi = point.PointOfInterests.Single();
                        sb.Append($"<img src='{_iconsetImagePath}{poi.Type.ToString().ToLower()}.png' alt='POI: {poi.Type}' class='icon muddle-map-point-of-interest'/>");
                    }

                    sb.Append("</td>");
                }

                sb.Append("</tr>");

            }

            sb.Append("</table>");
        
            output.TagName = "section";
            output.Content.SetHtmlContent(sb.ToString());
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
