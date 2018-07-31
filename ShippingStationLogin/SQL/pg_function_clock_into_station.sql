CREATE OR REPLACE FUNCTION clock_into_station(
    sh_id integer,
    station_number integer,
    sh_session_key integer)
  RETURNS boolean AS
$BODY$
BEGIN

    if exists(
        select 
          session_key 
        from 
          doarni.sh_ship_station_session 
        where 
          to_char(clock_in, 'YYYYMMDD') = to_char(current_timestamp, 'YYYYMMDD') 
          and session_key = sh_session_key
          and user_id = sh_id
          and station = station_number
    )
      
    then return false;
    END IF;
      INSERT into doarni.sh_ship_station_session (user_id, station, clock_in, clock_out, session_key)
        VALUES (SH_ID, STATION_NUMBER, current_timestamp, null, sh_session_key);
        RETURN TRUE;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION clock_into_station(integer, integer, integer)
  OWNER TO doarni;
GRANT EXECUTE ON FUNCTION clock_into_station(integer, integer, integer) TO public;
GRANT EXECUTE ON FUNCTION clock_into_station(integer, integer, integer) TO doarni;
GRANT EXECUTE ON FUNCTION clock_into_station(integer, integer, integer) TO reader;
