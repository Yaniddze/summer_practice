CREATE TABLE users
(
    id uuid PRIMARY KEY,
    email varchar(50),
    name varchar(20),
    isEmailConfirmed boolean DEFAULT false,
    login varchar(20),
    password varchar(40),
    token text,
    expiry_date TIMESTAMP,
    creation_date TIMESTAMP,
    JwtId text,
    activation_url uuid
);

ALTER TABLE users OWNER TO postgres;