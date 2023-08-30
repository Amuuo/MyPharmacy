CREATE PROCEDURE sp_SearchPharmacyList
    @SearchQuery   NVARCHAR(255) = NULL,
    @PageNumber    INT = 1,
    @PageSize      INT = 10,
    @SortColumn    NVARCHAR(50) = 'Id',
    @SortDirection NVARCHAR(4) = 'ASC'
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @StartRow INT = (@PageNumber - 1) * @PageSize + 1;
    DECLARE @EndRow   INT = @PageNumber * @PageSize;
    
    DECLARE @SQL NVARCHAR(2000);
    
    IF @SearchQuery IS NOT NULL
        SET @SearchQuery = '%' + @SearchQuery + '%';
    
    SET @SQL = N'
        WITH SearchResults AS (
            SELECT 
                *,
                ROW_NUMBER() OVER (ORDER BY ' + QUOTENAME(@SortColumn) + ' ' + @SortDirection + ') AS RowNum
            FROM 
                pharmacy
            WHERE 
                (@SearchQuery IS NULL 
                OR Name  LIKE @SearchQuery
                OR City  LIKE @SearchQuery
                OR State LIKE @SearchQuery
                OR Zip   LIKE @SearchQuery)
        )
        SELECT 
            * 
        FROM 
            SearchResults
        WHERE 
            RowNum BETWEEN @StartRow AND @EndRow;
    ';
    
    EXEC sp_executesql @SQL, 
        N'@SearchQuery NVARCHAR(255), @StartRow INT, @EndRow INT',
        @SearchQuery, @StartRow, @EndRow;
    
END;