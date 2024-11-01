drop schema if exists public cascade;
create schema public;
drop schema if exists library cascade;
create schema if not exists library;

CREATE TABLE library.books
(
    id               integer GENERATED BY DEFAULT AS IDENTITY,
    title            character varying(255) NOT NULL,
    author           character varying(255) NOT NULL,
    genre            character varying(50),
    created_at       timestamp with time zone DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT books_pkey PRIMARY KEY (id)
);

CREATE TABLE library.libraryuser
(
    id              integer GENERATED BY DEFAULT AS IDENTITY,
    name      character varying(100) NOT NULL,
    email           character varying(255) NOT NULL UNIQUE,
    phone           character varying(20),
    created_at      timestamp with time zone DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT libraryuser_pkey PRIMARY KEY (id)
);

CREATE TABLE library.loans
(
    id              integer GENERATED BY DEFAULT AS IDENTITY,
    book_id         integer NOT NULL,
    user_id         integer NOT NULL,
    return_date     timestamp with time zone,
    is_returned     boolean DEFAULT FALSE,
    created_at      timestamp with time zone DEFAULT (CURRENT_TIMESTAMP),
    CONSTRAINT book_loans_pkey PRIMARY KEY (id),
    CONSTRAINT fk_book FOREIGN KEY (book_id) REFERENCES library.books(id),
    CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES library.libraryuser(id)
);

