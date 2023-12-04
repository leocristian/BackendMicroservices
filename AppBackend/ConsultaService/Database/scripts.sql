
CREATE DATABASE "ConsultaServiceDB"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Portuguese_Brazil.1252'
    LC_CTYPE = 'Portuguese_Brazil.1252'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

-------------------------------------------------------------------------------------

CREATE TABLE IF NOT EXISTS public.pacientes
(
    id integer NOT NULL DEFAULT nextval('pacientes_id_seq'::regclass),
    nome_completo character varying(100) COLLATE pg_catalog."default" NOT NULL,
    email character varying(50) COLLATE pg_catalog."default",
    telefone character varying(11) COLLATE pg_catalog."default",
    data_nascimento date NOT NULL,
    cpf character varying(11) COLLATE pg_catalog."default" NOT NULL,
    endereco character varying(30) COLLATE pg_catalog."default",
    numero_sus character varying(40) COLLATE pg_catalog."default",
    CONSTRAINT pacientes_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.pacientes
    OWNER to postgres;

-------------------------------------------------------------------------------------

CREATE TABLE IF NOT EXISTS public.agendamentos
(
    id integer NOT NULL DEFAULT nextval('agendamentos_id_seq'::regclass),
    id_paciente integer NOT NULL DEFAULT 0,
    id_enfermeiro integer NOT NULL DEFAULT 0,
    descricao character varying(100) COLLATE pg_catalog."default" NOT NULL,
    data_hora timestamp without time zone NOT NULL,
    id_local integer NOT NULL,
    observacoes character varying(300) COLLATE pg_catalog."default",
    CONSTRAINT agendamentos_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.agendamentos
    OWNER to postgres;

-------------------------------------------------------------------------------------

CREATE TABLE IF NOT EXISTS public.locais
(
    id integer NOT NULL DEFAULT nextval('locais_id_seq'::regclass),
    nome character varying(50) COLLATE pg_catalog."default" NOT NULL,
    endereco character varying(150) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT locais_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.locais
    OWNER to postgres;