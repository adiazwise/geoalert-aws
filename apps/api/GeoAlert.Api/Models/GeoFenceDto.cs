using System.ComponentModel.DataAnnotations;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System.Text.Json.Serialization;
namespace GeoAlert.Api.Models;
using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("GeoFencesTable")]
public class GeoFenceDto
{
    [DynamoDBHashKey]
    public Guid Id { get; set; }
    [DynamoDBProperty]
    public string Address { get; set; } = string.Empty;
    [DynamoDBProperty]
    public double Latitude { get; set; }
    [DynamoDBProperty]
    public double Longitude { get; set; }
    [DynamoDBProperty]
    public string CreatedBy { get; set; } = string.Empty;
    [DynamoDBProperty]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}