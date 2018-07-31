CREATE TABLE sh_employee
(
  user_id integer,
  first_name character varying,
  last_name character varying,
  email character varying,
  active integer,
  works_inbound boolean,
  works_outbound boolean,
  is_management boolean,
  works_decon boolean,
  works_shipping boolean,
  CONSTRAINT sh_employee_sh_user_id_fk FOREIGN KEY (user_id)
      REFERENCES sh_user (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE
);
ALTER TABLE sh_employee
  OWNER TO doarni;
GRANT ALL ON TABLE sh_employee TO doarni;
GRANT SELECT ON TABLE sh_employee TO reader;
COMMENT ON TABLE sh_employee
  IS 'Employee table for Southaven Implat Bank';