CREATE VIEW vw_warehouse_profit AS
SELECT 
    w.name             warehouse,
    SUM(d.total_price) total_delivery_revenue,
    SUM(d.unit_count)  total_unit_count,
    SUM(d.total_price) / SUM(d.unit_count) average_profit_per_unit
FROM 
    delivery d
JOIN 
    warehouse w ON d.warehouse_id = w.id
GROUP BY 
    w.name
