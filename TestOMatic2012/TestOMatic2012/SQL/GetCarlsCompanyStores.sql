USE [INFO2000]


SELECT [c_unit_no]
  FROM [dbo].[ps_c_unit_master] p
  WHERE c_unit_no LIKE '11%'
  AND c_owner = 'C'
  AND eff_status = 'A'
  AND effdt = (SELECT MAX(effdt) FROM [dbo].[ps_c_unit_master] WHERE c_unit_no = p.c_unit_no)
  ORDER BY c_unit_no



