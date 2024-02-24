using DataBase.Base;

namespace FrontEnd.Constants;

public static class IgnoreProperties
{
    public static readonly string BaseEntity = string.Join(" ", typeof(BaseEntity).GetProperties().Select(x=> x.Name));
}