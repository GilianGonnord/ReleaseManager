﻿using System.Text.Json;

namespace ReleaseManager.Api.Policies.JsonNaming;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public static SnakeCaseNamingPolicy Instance { get; } = new SnakeCaseNamingPolicy();

    public static string ToSnakeCase(string str) => string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();

    public override string ConvertName(string name) => ToSnakeCase(name);
}
