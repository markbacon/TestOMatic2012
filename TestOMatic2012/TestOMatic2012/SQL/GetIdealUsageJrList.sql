SELECT	 jrno
		,SUM(qty) AS QuantitySold
FROM	fcmenusold
WHERE	wedate = '!WEEK_END_DATE'
GROUP BY jrno
ORDER BY jrno
