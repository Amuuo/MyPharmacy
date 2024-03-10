UPDATE Pharmacy

SET 
    Name = @Name, 
    Address = @Address, 
    City = @City, 
    State = @State, 
    Zip = @Zip, 
    PrescriptionsFilled = @PrescriptionsFilled, 
    UpdatedDate = CURRENT_TIMESTAMP
WHERE 
    Id = @Id;

SELECT LAS