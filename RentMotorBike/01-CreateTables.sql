CREATE DATABASE base;

\c base;


create table IF NOT EXISTS "motor_bike" (
Id  int  primary key GENERATED ALWAYS AS IDENTITY,
"Plate" varchar(7),
"ReleaseDate" Date,
"Model" varchar(50),
"DateCreated" Date,
"DateUpdated" Date
);


