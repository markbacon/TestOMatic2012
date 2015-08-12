UPDATE pos_fact
   SET total_cpn_amt = total_cpn_amt + CONVERT(NUMERIC(18, 2), '!SCANNED_COUPON_AMOUNT'),
	   total_cpn_cnt = total_cpn_cnt + CONVERT(INT, '!SCANNED_COUPON_COUNT'),
	   total_gs_amt = total_gs_amt  + CONVERT(NUMERIC(18, 2), '!SCANNED_COUPON_AMOUNT'),
	   last_chg_by = 'ckrcorp\mbacon',
	   last_chg_date = GETDATE()

WHERE pos_fact_id = CONVERT(INT, '!POS_FACT_ID')