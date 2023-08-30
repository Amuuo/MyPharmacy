CREATE TABLE dbo.warehouse (
    id      INT            IDENTITY (1, 1) NOT NULL,
    name    NVARCHAR(100) NOT NULL,
    address NVARCHAR(100) NOT NULL,
    city    NVARCHAR(50)  NOT NULL,
    state   NVARCHAR(2)   NOT NULL,
    zip     NVARCHAR(20)  NOT NULL,
    CONSTRAINT PK_warehouse PRIMARY KEY CLUSTERED (id ASC)
);
