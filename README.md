To use this project add in secrets this information:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=ip;Database=name;User Id=name;Password=password;Integrated Security=false; TrustServerCertificate=Yes"
  },
  "Jwt:Key": "SecretToken",
  "Jwt:Issuer": "Issuer Address",
  "Jwt:Audience": "Audience Address",
  "Jwt:ExpirationSeconds": JWT Expiration time in seconds 
}
