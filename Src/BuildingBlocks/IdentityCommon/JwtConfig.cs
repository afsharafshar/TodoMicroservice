﻿namespace IdentityCommon;

public class JwtConfig
{
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenExpiry { get; set; }
    public int RefreshTokenExpiry { get; set; }
}