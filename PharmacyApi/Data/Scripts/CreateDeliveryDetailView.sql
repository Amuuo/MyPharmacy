CREATE view vw_delivery_detail AS
SELECT 
    w.name warehouse_from,
    p.name pharmacy_to,
    d.drug_name,
    d.unit_count,
    d.unit_price,
    d.total_price,
    d.delivery_date
FROM 
    delivery d
JOIN warehouse w ON d.warehouse_id = w.id
JOIN pharmacy p ON d.pharmacy_id = p.id;