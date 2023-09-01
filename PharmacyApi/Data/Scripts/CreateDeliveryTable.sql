CREATE TABLE delivery (
    id            INT            IDENTITY (1, 1) NOT NULL,
    warehouse_id  INT            NOT NULL,
    pharmacy_id   INT            NOT NULL,
    drug_name     NVARCHAR (100) NOT NULL,
    unit_count    INT            NOT NULL,
    unit_price    MONEY          NOT NULL,
    total_price   AS (unit_count * unit_price),
    delivery_date DATE           NOT NULL,
    CONSTRAINT FK_delivery_pharmacy FOREIGN KEY (pharmacy_id) REFERENCES pharmacy (Id),
    CONSTRAINT FK_delivery_warehouse FOREIGN KEY (warehouse_id) REFERENCES warehouse (id)
);
