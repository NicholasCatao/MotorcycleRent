CREATE DATABASE base;

\c base;

create table "motor_bike" (
"Id"  SERIAL,
"Plate" varchar(7),
"Year" Date,
"Model" varchar(50),
"DateCreated" Date,
"DateUpdated" Date,
CONSTRAINT "PK_Plate" PRIMARY KEY ("Plate")
);


