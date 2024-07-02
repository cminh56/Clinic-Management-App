USE [PRN221_ProjectClinicManagement1]
GO

INSERT INTO [dbo].[Account]
           ([Email]
           ,[UserName]
           ,[Password]
           ,[Role]
           ,[Status]
           ,[Salary]
           ,[JoinDate])
VALUES
           ('user1@example.com', 'Admin', '$2a$11$tUra/L8Tjk3GkECoFDo9O.ajG1ctiqAEVHwQuGO0J/QGItiz4ICIG', 0, 1, 5000.00, '2023-01-01 00:00:00'),
           ('user2@example.com', 'Doctor', '$2a$11$tUra/L8Tjk3GkECoFDo9O.ajG1ctiqAEVHwQuGO0J/QGItiz4ICIG', 1, 1, 6000.00, '2023-02-01 00:00:00'),
           ('user3@example.com', 'Nurse', '$2a$11$tUra/L8Tjk3GkECoFDo9O.ajG1ctiqAEVHwQuGO0J/QGItiz4ICIG', 2, 1, 5500.00, '2023-03-01 00:00:00'),
           ('user4@example.com', 'Receipter', '$2a$11$tUra/L8Tjk3GkECoFDo9O.ajG1ctiqAEVHwQuGO0J/QGItiz4ICIG', 3, 1, 6200.00, '2023-04-01 00:00:00'),
           ('user5@example.com', 'User5', '$2a$11$tUra/L8Tjk3GkECoFDo9O.ajG1ctiqAEVHwQuGO0J/QGItiz4ICIG', 0, 1, 5300.00, '2023-05-01 00:00:00'),
           ('user6@example.com', 'User6', '$2a$11$tUra/L8Tjk3GkECoFDo9O.ajG1ctiqAEVHwQuGO0J/QGItiz4ICIG', 1, 1, 6400.00, '2023-06-01 00:00:00'),
           ('user7@example.com', 'User7', '$2a$11$tUra/L8Tjk3GkECoFDo9O.ajG1ctiqAEVHwQuGO0J/QGItiz4ICIG', 1, 1, 5100.00, '2023-07-01 00:00:00')
    
   -- Mk : 123       
GO
INSERT INTO [dbo].[Medicines]
           ([Name]
           ,[ATCCode]
           ,[GenericName]
           ,[Description]
           ,[Manufacturer]
           ,[Type]
           ,[Category]
           ,[Unit]
           ,[Price]
           ,[Quantity]
           ,[Status])
VALUES
           ('Aspirin', 'A01AD05', 'Acetylsalicylic Acid', 'Used to reduce pain, fever, or inflammation.', 'Bayer', 'Tablet', 'Analgesics', 'Box', 5.99, 100, 1),
           ('Paracetamol', 'N02BE01', 'Acetaminophen', 'Used to treat mild to moderate pain and fever.', 'Johnson & Johnson', 'Tablet', 'Antipyretics', 'Box', 4.50, 200, 1),
           ('Ibuprofen', 'M01AE01', 'Ibuprofen', 'Nonsteroidal anti-inflammatory drug used for pain relief.', 'Pfizer', 'Tablet', 'Anti-inflammatory', 'Bottle', 7.20, 150, 1),
           ('Amoxicillin', 'J01CA04', 'Amoxicillin', 'Antibiotic used to treat bacterial infections.', 'GlaxoSmithKline', 'Capsule', 'Antibiotics', 'Box', 12.30, 120, 1),
           ('Ciprofloxacin', 'J01MA02', 'Ciprofloxacin', 'Antibiotic used to treat various bacterial infections.', 'Bayer', 'Tablet', 'Antibiotics', 'Box', 14.75, 80, 1),
           ('Cetirizine', 'R06AE07', 'Cetirizine', 'Antihistamine used to relieve allergy symptoms.', 'Novartis', 'Tablet', 'Antihistamines', 'Strip', 8.99, 160, 1),
           ('Metformin', 'A10BA02', 'Metformin', 'Used to improve blood sugar control in people with type 2 diabetes.', 'Merck', 'Tablet', 'Antidiabetics', 'Bottle', 10.00, 180, 1),
           ('Lisinopril', 'C09AA03', 'Lisinopril', 'Used to treat high blood pressure and heart failure.', 'AstraZeneca', 'Tablet', 'Antihypertensives', 'Box', 9.60, 130, 1),
           ('Omeprazole', 'A02BC01', 'Omeprazole', 'Used to treat gastroesophageal reflux disease and other stomach issues.', 'Sanofi', 'Capsule', 'Proton Pump Inhibitors', 'Box', 11.40, 90, 1),
           ('Simvastatin', 'C10AA01', 'Simvastatin', 'Used to lower cholesterol and triglycerides in the blood.', 'Merck', 'Tablet', 'Statins', 'Bottle', 13.25, 110, 1)


INSERT INTO [dbo].[Patients]
           ([Name]
           ,[Age]
           ,[Weight]
           ,[Height]
           ,[Phone]
           ,[Email]
           ,[Address]
           ,[Emergency]
           ,[Status])
VALUES
           ('John Doe', 45, 78.5, 180.3, '123-456-7890', 'johndoe@example.com', '123 Main St, Anytown, USA', 'Jane Doe', 1),
           ('Jane Smith', 34, 65.0, 165.0, '234-567-8901', 'janesmith@example.com', '456 Elm St, Anytown, USA', 'John Smith', 1),
           ('Michael Johnson', 50, 92.3, 175.5, '345-678-9012', 'michaelj@example.com', '789 Oak St, Anytown, USA', 'Sarah Johnson', 1),
           ('Emily Davis', 28, 54.6, 160.2, '456-789-0123', 'emilyd@example.com', '101 Pine St, Anytown, USA', 'David Davis', 1),
           ('Daniel Wilson', 63, 85.4, 172.1, '567-890-1234', 'danielw@example.com', '202 Maple St, Anytown, USA', 'Laura Wilson', 1),
           ('Sophia Martinez', 41, 70.2, 167.5, '678-901-2345', 'sophiam@example.com', '303 Birch St, Anytown, USA', 'Carlos Martinez', 1),
           ('James Brown', 36, 88.8, 183.0, '789-012-3456', 'jamesb@example.com', '404 Cedar St, Anytown, USA', 'Linda Brown', 1),
           ('Olivia Garcia', 29, 58.7, 159.3, '890-123-4567', 'oliviag@example.com', '505 Walnut St, Anytown, USA', 'Miguel Garcia', 1),
           ('Alexander Miller', 52, 94.1, 178.9, '901-234-5678', 'alexm@example.com', '606 Chestnut St, Anytown, USA', 'Maria Miller', 1),
           ('Isabella Hernandez', 47, 72.9, 164.7, '012-345-6789', 'isabellah@example.com', '707 Spruce St, Anytown, USA', 'Juan Hernandez', 1)

USE [PRN221_ProjectClinicManagement1]
GO

INSERT INTO [dbo].[Patient_Records]
           ([PatientId]
           ,[DoctorId]
           ,[Disease]
           ,[Symptoms]
           ,[Diagnosis]
           ,[Date])
VALUES
           (1,2, 'Hypertension', 'Headache, dizziness', 'Confirmed hypertension, prescribed medication', '2023-01-15 09:30:00'),
           (2, 2, 'Diabetes', 'Frequent urination, increased thirst', 'Confirmed type 2 diabetes, prescribed insulin', '2023-02-10 10:00:00'),
           (3, 2, 'Asthma', 'Shortness of breath, wheezing', 'Confirmed asthma, prescribed inhaler', '2023-03-12 11:15:00'),
           (4, 2, 'Migraine', 'Severe headache, sensitivity to light', 'Confirmed migraine, prescribed pain relief', '2023-04-08 14:45:00'),
           (5, 2, 'Arthritis', 'Joint pain, stiffness', 'Confirmed rheumatoid arthritis, prescribed anti-inflammatory', '2023-05-22 08:20:00'),
           (6, 2, 'Allergy', 'Sneezing, itchy eyes', 'Confirmed pollen allergy, prescribed antihistamines', '2023-06-14 09:50:00'),
           (7, 2, 'Back Pain', 'Lower back pain, difficulty standing', 'Diagnosed with lumbar strain, prescribed physical therapy', '2023-07-19 13:40:00'),
           (8, 2, 'Flu', 'Fever, cough, body aches', 'Confirmed influenza, prescribed rest and fluids', '2023-08-23 12:30:00'),
           (9, 2, 'High Cholesterol', 'No symptoms', 'Diagnosed with hyperlipidemia, prescribed statins', '2023-09-30 15:25:00'),
           (10, 2, 'GERD', 'Heartburn, acid reflux', 'Confirmed gastroesophageal reflux disease, prescribed antacids', '2023-10-05 16:10')


INSERT INTO [dbo].[Prescriptions]
           ([PatientRecordId]
           ,[Dosage]
           ,[Duration]
           ,[Instruction]
           ,[Remark]
           ,[Date])
VALUES
           (1, '10mg', '30 days', 'Take one tablet daily', 'Monitor blood pressure', '2023-01-15 10:00:00'),
           (2, '20 units', 'Indefinite', 'Administer subcutaneously before meals', 'Monitor blood sugar levels', '2023-02-10 10:30:00'),
           (3, '2 puffs', 'As needed', 'Use inhaler during asthma attack', 'Carry inhaler at all times', '2023-03-12 11:45:00'),
           (4, '50mg', 'As needed', 'Take one tablet at onset of headache', 'Avoid bright lights', '2023-04-08 15:00:00'),
           (5, '200mg', 'Daily', 'Take one tablet twice daily', 'Monitor joint pain', '2023-05-22 08:45:00'),
           (6, '10mg', 'As needed', 'Take one tablet during allergic reaction', 'Avoid allergens', '2023-06-14 10:00:00'),
           (7, '15mg', '2 weeks', 'Apply cream to affected area twice daily', 'Avoid heavy lifting', '2023-07-19 14:00:00'),
           (8, '500mg', '7 days', 'Take one tablet twice daily', 'Drink plenty of fluids', '2023-08-23 13:00:00'),
           (9, '40mg', 'Daily', 'Take one tablet at bedtime', 'Maintain a low-fat diet', '2023-09-30 16:00:00'),
           (10, '20mg', 'As needed', 'Take one tablet after meals', 'Avoid spicy foods', '2023-10-05 16:30:00')



INSERT INTO [dbo].[Prescription_Medicine]
           ([MedicineID]
           ,[PrescriptionID]
           ,[Quantity]
           ,[Unit]
		   )
VALUES
           (1, 1, 30, 'Tablets'),
           (2, 2, 60, 'Units'),
           (3, 3, 1, 'Inhaler'),
           (4, 4, 10, 'Tablets'),
           (5, 5, 60, 'Tablets'),
           (6, 6, 30, 'Tablets'),
           (7, 7, 1, 'Tube'),
           (8, 8, 14, 'Tablets'),
           (9, 9, 30, 'Tablets'),
           (10, 10, 30, 'Tablets')

INSERT INTO [dbo].[Receipts]
           ([PatientId]
           ,[TotalAmount]
           ,[ReceptionistId]
           ,[Date]
           ,[Status])
VALUES
           (1, 150.00, 2, '2023-01-15 11:00:00', 1),
           (2, 200.50, 2, '2023-02-10 11:30:00', 1),
           (3, 120.75, 2, '2023-03-12 12:00:00', 1),
           (4, 95.00, 2, '2023-04-08 15:30:00', 1),
           (5, 175.25, 2, '2023-05-22 09:00:00', 1),
           (6, 60.00, 2, '2023-06-14 10:30:00', 1),
           (7, 110.00, 2, '2023-07-19 14:30:00', 1),
           (8, 80.50, 2, '2023-08-23 13:30:00', 1),
           (9, 250.00, 2, '2023-09-30 16:30:00', 1),
           (10, 140.75, 2, '2023-10-05 17:00:00', 1)
GO
