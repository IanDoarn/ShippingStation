CREATE TABLE sh_ship_station_session
(
  user_id integer,
  station integer,
  clock_in timestamp with time zone,
  clock_out timestamp with time zone,
  session_key integer
)
WITH (
  OIDS=FALSE
);
ALTER TABLE sh_ship_station_session
  OWNER TO doarni;
GRANT ALL ON TABLE sh_ship_station_session TO doarni;
GRANT SELECT ON TABLE sh_ship_station_session TO reader;
COMMENT ON TABLE sh_ship_station_session
  IS 'Logs user activity at shipping stations';