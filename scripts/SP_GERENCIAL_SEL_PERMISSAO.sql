
Create Procedure SP_GERENCIAL_SEL_PERMISSAO
	@Valor	varchar(150),
	@TipoId uniqueidentifier,
	@Skip int,
	@PageSize int,
	@SortColumn int,
	@SorDirect varchar(4) = 'DESC'
AS
	SET NOCOUNT ON;

	SELECT 
		p.Id, p.Valor, p.TipoId, 'Separador' Separador, T.Id, t.Nome
	FROM 
		Permissao p
		inner join tipopermissao t on p.TipoId = t.Id
	Where 
		Valor like '%'+@Valor+'%'
		and (@TipoId is null or TipoId = @TipoId)
	ORDER BY
		CASE WHEN @SortColumn = 2 AND @SorDirect = 'desc' THEN Valor END DESC,    
        CASE WHEN @SortColumn = 2 AND @SorDirect = 'asc' THEN Valor END ASC
	OFFSET @Skip ROWS
	FETCH NEXT @PageSize ROWS ONLY;

	SELECT Count(*) as TotalRegs from Permissao Where Valor like '%'+@Valor+'%' and (@TipoId is null or TipoId = @TipoId);
GO

SP_GERENCIAL_SEL_PERMISSAO '', null, 0, 15, 1, 'asc'
