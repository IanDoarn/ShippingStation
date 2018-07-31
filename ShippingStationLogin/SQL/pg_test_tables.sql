/*
   Examples of clocking in and out functions
  */

select doarni.clock_into_station(27, 1, 1997);
select doarni.clock_out_station(27, 1, 1997);

select doarni.clock_into_station(6, 1, 123);
select doarni.clock_into_station(12, 1, 456);
select doarni.clock_into_station(20, 1, 789);


select doarni.clock_out_station(11, 1, 1997);
select doarni.clock_out_station(22, 4, 1997);

select doarni.clock_out_station(20, 1, 789);

/*
  Get today's shipping activity
 */
SELECT
  e.first_name, e.last_name,
  shst.clock_in, shst.clock_out,
  to_char(shst.clock_out - shst.clock_in, 'MI:SS') as worked_time
FROM
  doarni.sh_ship_station_session shst
  LEFT JOIN doarni.sh_employee e on shst.user_id = e.user_id
WHERE
  to_char(shst.clock_in, 'YYYY-MM-DD') = to_char(current_timestamp, 'YYYY-MM-DD')
  and to_char(shst.clock_out, 'YYYY-MM-DD') = to_char(current_timestamp, 'YYYY-MM-DD');

/*
  Get employees who work at shipping station
 */
select
  user_id,
  first_name,
  last_name
from
  doarni.sh_employee
where
  works_shipping is true
  and active = 1;


/*
  Get employees who are currently shipping
 */
SELECT
  e.user_id,
  e.first_name,
  e.last_name,
  shst.station,
  shst.clock_in,
  shst.session_key
FROM
  sh_employee e
  LEFT JOIN sh_ship_station_session shst on e.user_id = shst.user_id
WHERE
  to_char(shst.clock_in, 'YYYY-MM-DD') = to_char(current_timestamp, 'YYYY-MM-DD')
  and shst.clock_out is null;

/*
  Get station status
 */
SELECT
  e.*,
  shst.*
FROM
  sh_employee e
  LEFT JOIN sh_ship_station_session shst on e.user_id = shst.user_id
WHERE
  to_char(shst.clock_in, 'YYYY-MM-DD') = to_char(current_timestamp, 'YYYY-MM-DD')
  and shst.clock_out is null
  and shst.station = 1;

/*
  Get number of employees at a given station
 */
SELECT
  coalesce(shst.station, null) as station,
  coalesce(count(*), null) as number_at_station
FROM
  doarni.sh_ship_station_session shst
WHERE
  to_char(shst.clock_in, 'YYYY-MM-DD') = to_char(current_timestamp, 'YYYY-MM-DD')
  and shst.clock_out is null
  and shst.station = 1
GROUP BY
  shst.station