CREATE VIEW vw_pharmacist_sales_summary AS
WITH primary_rx_sales AS (
    SELECT 
        ph.id pharmacist_id,
        SUM(d.unit_count) primary_unit_count
    FROM 
        pharmacist ph    
    JOIN pharmacy_pharmacist pp ON ph.id = pp.pharmacist_id
    JOIN delivery d             ON d.pharmacy_id = pp.pharmacy_id
    WHERE 
        d.drug_name = ph.primary_rx
    GROUP BY 
        ph.id
),
non_primary_rx_sales AS (
    SELECT 
        ph.id pharmacist_id,
        SUM(d.unit_count) non_primary_unit_count
    FROM 
        pharmacist ph
    JOIN pharmacy_pharmacist pp  ON ph.id = pp.pharmacist_id
    JOIN delivery d              ON d.pharmacy_id = pp.pharmacy_id
    WHERE 
        d.drug_name != ph.primary_rx
    GROUP BY 
        ph.id
),
pharmacy_aggregation AS (
    SELECT 
        pp.pharmacist_id,
        STRING_AGG(p.name, ', ') AS pharmacyList
    FROM 
        pharmacy_pharmacist pp
    JOIN pharmacy p ON pp.pharmacy_id = p.id
    GROUP BY 
        pp.pharmacist_id
)

SELECT 
    ph.id pharmacist_id,
    ph.first_name,
    ph.last_name,
    pa.pharmacyList pharmacy_list,
    ph.primary_rx primary_rx,
    COALESCE(pds.primary_unit_count, 0) total_primary_units,
    COALESCE(npds.non_primary_unit_count, 0) total_non_primary_units
FROM 
    pharmacist ph
JOIN pharmacy_aggregation pa        ON pa.pharmacist_id = ph.id
LEFT JOIN primary_rx_sales pds      ON ph.id = pds.pharmacist_id
LEFT JOIN non_primary_rx_sales npds ON ph.id = npds.pharmacist_id;