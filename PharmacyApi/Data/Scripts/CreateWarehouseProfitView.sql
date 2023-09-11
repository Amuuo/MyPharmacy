CREATE VIEW vw_warehouse_profit AS
WITH warehouse_revenues AS (
    SELECT     
        w.id               warehouse_id,
        SUM(d.total_price) total_delivery_revenue,
        SUM(d.unit_count)  total_unit_count,
        SUM(d.total_price) / SUM(d.unit_count) average_profit_per_unit
    FROM 
        delivery d
    JOIN 
        warehouse w ON d.warehouse_id = w.id
    GROUP BY 
        w.id
)

SELECT 
    w.name warehouse,
    wr.total_delivery_revenue,
    wr.total_unit_count,
    wr.average_profit_per_unit
FROM 
    warehouse_revenues wr
JOIN warehouse w ON wr.warehouse_id = w.id