CREATE TABLE sh_user
(
  id integer,
  sms_user_id integer,
  sms_username character varying,
  CONSTRAINT sh_user_id_pk UNIQUE (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE sh_user
  OWNER TO doarni;
GRANT ALL ON TABLE sh_user TO doarni;
GRANT SELECT ON TABLE sh_user TO reader;
COMMENT ON TABLE sh_user
  IS 'User table for Southaven Implant Bank';