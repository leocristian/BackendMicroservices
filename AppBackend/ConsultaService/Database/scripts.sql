CREATE DATABASE "ConsultaServiceDB"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

-------------------------------------------------------------------------------------
drop table pacientes;
CREATE TABLE IF NOT EXISTS public.pacientes
(
    id serial NOT NULL, 
    nomecompleto character varying(100) NOT NULL,
    email character varying(50),
    telefone character varying(11),
    datanascimento date NOT NULL,
    cpf character varying(11),
    endereco character varying(30),
    numerosus character varying(40),
    CONSTRAINT pacientes_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.pacientes
    OWNER to postgres;

-------------------------------------------------------------------------------------
drop table agendamentos;
CREATE TABLE IF NOT EXISTS public.agendamentos
(
    id serial NOT NULL,
    idpaciente integer NOT NULL DEFAULT 0,
    idenfermeiro integer NOT NULL DEFAULT 0,
    descricao character varying(100) COLLATE pg_catalog."default" NOT NULL,
    data_hora timestamp without time zone NOT NULL,
  	dataconsulta date,
  	horaconsulta time,
    idlocal integer NOT NULL,
    observacoes character varying(300) COLLATE pg_catalog."default",
  	status varchar(1),
    CONSTRAINT agendamentos_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.agendamentos
    OWNER to postgres;

-------------------------------------------------------------------------------------

drop table locais;
CREATE TABLE IF NOT EXISTS public.locais
(
    id integer NOT NULL,
    nome character varying(50) COLLATE pg_catalog."default" NOT NULL,
    endereco character varying(150) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT locais_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.locais
    OWNER to postgres;