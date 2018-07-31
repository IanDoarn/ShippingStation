CREATE OR REPLACE FUNCTION clock_out_station(
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
          and clock_out is null
    )
    THEN
      UPDATE
        doarni.sh_ship_station_session
      SET
        clock_out = current_timestamp, session_key = null
      WHERE
        to_char(clock_in, 'YYYYMMDD') = to_char(current_timestamp, 'YYYYMMDD')
        and user_id = sh_id
        and station = station_number
        and session_key = sh_session_key;

      RETURN TRUE;
    end if;
      return FALSE;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION clock_out_station(integer, integer, integer)
  OWNER TO doarni;
GRANT EXECUTE ON FUNCTION clock_out_station(integer, integer, integer) TO public;
GRANT EXECUTE ON FUNCTION clock_out_station(integer, integer, integer) TO doarni;
GRANT EXECUTE ON FUNCTION clock_out_station(integer, integer, integer) TO reader;
