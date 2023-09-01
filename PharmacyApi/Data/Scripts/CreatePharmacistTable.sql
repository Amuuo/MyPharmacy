CREATE TABLE pharmacist (
    id          INT           IDENTITY (1, 1) NOT NULL,
    pharmacy_id INT           NOT NULL,
    first_name  NVARCHAR(50) NOT NULL,
    last_name   NVARCHAR(50) NOT NULL,
    age         INT           NOT NULL,
    hire_date   DATE          NOT NULL,
    primary_rx  NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_pharmacist PRIMARY KEY CLUSTERED (id ASC),
    CONSTRAINT FK_pharmacist_pharmacy FOREIGN KEY (pharmacy_id) REFERENCES pharmacy (Id)
);
