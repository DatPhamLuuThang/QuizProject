namespace FrontEnd.Constants;

public static class ViewPath
{
    private const string Format = ".cshtml";
    private const string StaticView = "~/Views/Static/";

    private const string ButtonPartialView = $"{StaticView}Button/";
    private const string StaticPartialView = $"{StaticView}Partial/";

    /* Static View */
    public const string DataTableView = $"{StaticView}DataTableView{Format}";

    public const string FormView = $"{StaticView}FormView{Format}";

    /* Static Partial View */
    public const string PaginationNav = $"{StaticPartialView}_paginationNav{Format}";

    public const string FilterForm = $"{StaticPartialView}_filterForm{Format}";
    
    public const string EmptyTable = $"{StaticPartialView}_emptyTable{Format}";

    /* Static Button Partial View */
    public const string ButtonRemoveUpdateDetails = $"{ButtonPartialView}_btnGroupRUD{Format}";

    //public const string ButtonDetails = $"{ButtonPartialView}_btnDetails{Format}";

    public const string ButtonDelete = $"{ButtonPartialView}_btnDelete{Format}";

    public const string ButtonGroupHeader = $"{ButtonPartialView}_btnGroupHeader{Format}";
}