--start HRA script header
INSERT INTO [tblScriptsRun] ([dateRun],[scriptDate],[scriptName])
VALUES (GETDATE(),'2016-01-28',N'03 v_ApptListToday.sql')
GO
--end HRA script header

/****** Object:  View [dbo].[v_ApptListToday]    Script Date: 01/28/2016 11:40:03 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_ApptListToday]'))
DROP VIEW [dbo].[v_ApptListToday]
GO

/****** Object:  View [dbo].[v_ApptListToday]    Script Date: 01/28/2016 11:40:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[v_ApptListToday]
AS
SELECT     TOP (100) PERCENT dbo.tblAppointments.appttime AS [Appt Time], dbo.tblAppointments.unitnum AS MRN, dbo.tblAppointments.patientname AS [Patient Name], 
                      dbo.tblAppointments.dob, dbo.lkpSurveys.surveyName AS [Survey Type], dbo.tblAppointments.apptid, dbo.tblAppointments.clinicID, 
                      dbo.tblAppointments.imported
FROM         dbo.tblAppointments INNER JOIN
                      dbo.lkpSurveys ON dbo.tblAppointments.surveyID = dbo.lkpSurveys.surveyID
WHERE     (CONVERT(datetime, dbo.tblAppointments.apptdate) = DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0)) AND (dbo.tblAppointments.riskdatacompleted IS NULL) AND 
                      (dbo.tblAppointments.unitnum NOT LIKE '%?%')
ORDER BY CONVERT(datetime, dbo.tblAppointments.appttime)

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[26] 4[35] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tblAppointments"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 188
               Right = 257
            End
            DisplayFlags = 280
            TopColumn = 42
         End
         Begin Table = "lkpSurveys"
            Begin Extent = 
               Top = 6
               Left = 295
               Bottom = 152
               Right = 567
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1800
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_ApptListToday'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_ApptListToday'
GO


