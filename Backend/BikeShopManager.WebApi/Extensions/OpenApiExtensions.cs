using System.Reflection;
using System.Text.Json.Nodes;
using BikeShopManager.WebApi.ScalarAttributes;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace BikeShopManager.WebApi.Extensions;

public static class OpenApiExtensions
{
    public static OpenApiOptions AddOpenApiTagGroups(this OpenApiOptions options)
    {
        options.AddDocumentTransformer(
            (document, context, ct) =>
            {
                var tagGroups = context
                    .DescriptionGroups.SelectMany(g => g.Items)
                    .Select(d => d.ActionDescriptor)
                    .OfType<ControllerActionDescriptor>()
                    .Select(a =>
                        (
                            Group: a.ControllerTypeInfo.GetCustomAttribute<TagGroupAttribute>()?.Name,
                            Tag: a.EndpointMetadata.OfType<ITagsMetadata>()
                                .SelectMany(t => t.Tags)
                                .FirstOrDefault()
                                ?? a.ControllerName
                        )
                    )
                    .Where(x => x.Group is not null)
                    .Distinct()
                    .GroupBy(x => x.Group!)
                    .Select(g => new JsonObject
                    {
                        ["name"] = g.Key,
                        ["tags"] = new JsonArray(
                            g.Select(x => (JsonNode)JsonValue.Create(x.Tag)!).ToArray()
                        ),
                    })
                    .ToArray();

                document.Extensions ??= new Dictionary<string, IOpenApiExtension>();
                document.Extensions["x-tagGroups"] = new JsonNodeExtension(
                    new JsonArray(tagGroups)
                );
                return Task.CompletedTask;
            }
        );

        return options;
    }
}
