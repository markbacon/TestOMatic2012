USE [INFO2000]

SELECT hdr.mbm_date
	   ,hdr.restaurant_no
	   ,hdr.mbm_inv_no
	   ,hdr.system_id
	  ,dtl.[mbm_rcpt_dtl_id]
      ,dtl.[mbm_rcpt_hdr_id]
      ,dtl.[inv_xref_id]
      ,dtl.[inv_item_id]
      ,dtl.[mbm_cost]
      ,dtl.[mbm_qty]
      ,dtl.[mbm_amt]
      ,dtl.[match_status]
      ,dtl.[match_inv_id]
      ,dtl.[create_date]
      ,dtl.[create_by]
      ,dtl.[last_chg_date]
      ,dtl.[last_chg_by]
      ,dtl.[source]
  FROM [dbo].[mbm_rcpt_dtl_fact] dtl
  INNER JOIN mbm_rcpt_hdr_fact hdr ON dtl.mbm_rcpt_hdr_id = hdr.mbm_rcpt_hdr_id
  WHERE mbm_date >= '2015-04-22'
  AND mbm_qty < 0
  AND mbm_cost < 0
  ORDER BY mbm_date, restaurant_no


