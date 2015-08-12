USE [INFO2000]

UPDATE [dbo].[mbm_rcpt_dtl_fact]
   SET [mbm_cost] = !MBM_COST
 WHERE [mbm_rcpt_dtl_id] = !MBM_RCPT_DTL_ID

