CREATE TABLE user_table (
    userid INT PRIMARY KEY DEFAULT nextval('userid_sequence'),
    nome VARCHAR(35) NOT NULL,
    stars INT NOT NULL,
    purplestars INT NOT NULL
);

CREATE SEQUENCE userid_sequence START WITH 1;