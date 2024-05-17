namespace ErrorNpgsql.Models;

public class MyEntity
{
    public int Id { get; set; }

    public List<List<JsonbField>>? JsonbFields { get; set; }

}