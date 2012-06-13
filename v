[1mdiff --git a/src/Cashlinx/Common/Libraries/Utility/Shared/ReportsConstants.cs b/src/Cashlinx/Common/Libraries/Utility/Shared/ReportsConstants.cs[m
[1mindex 64f4d47..430f160 100644[m
[1m--- a/src/Cashlinx/Common/Libraries/Utility/Shared/ReportsConstants.cs[m
[1m+++ b/src/Cashlinx/Common/Libraries/Utility/Shared/ReportsConstants.cs[m
[36m@@ -93,12 +93,12 @@[m [mpublic class ReportConstants[m
         {[m
                 "Full Locations", "In Pawn Jewelry Locations",[m
                 "Loan Audit", "Snapshot", "CACC Sales Analysis", "Jewelry Count Detail", "Daily Sales",[m
[31m-                "Firearm Reports"//, "Refurb List"[m
[32m+[m[32m                "Firearm Reports", "Refurb List"[m
         };[m
 [m
         public static int[] DailyNumbers = new int[][m
         {[m
[31m-                209, 213, 206, 211, 219, 217, 227, 230//, 231[m
[32m+[m[32m                209, 213, 206, 211, 219, 217, 227, 230, 231[m
         };[m
 [m
         public static string[] MonthlyTitles = new string[][m
[1mdiff --git a/src/Cashlinx/Pawn/Pawn.csproj b/src/Cashlinx/Pawn/Pawn.csproj[m
[1mindex 6e9cb19..44ff913 100644[m
[1m--- a/src/Cashlinx/Pawn/Pawn.csproj[m
[1m+++ b/src/Cashlinx/Pawn/Pawn.csproj[m
[36m@@ -14,10 +14,14 @@[m
     <TargetFrameworkProfile>[m
     </TargetFrameworkProfile>[m
     <FileAlignment>512</FileAlignment>[m
[31m-    <SccProjectName></SccProjectName>[m
[31m-    <SccLocalPath></SccLocalPath>[m
[31m-    <SccAuxPath></SccAuxPath>[m
[31m-    <SccProvider></SccProvider>[m
[32m+[m[32m    <SccProjectName>[m
[32m+[m[32m    </SccProjectName>[m
[32m+[m[32m    <SccLocalPath>[m
[32m+[m[32m    </SccLocalPath>[m
[32m+[m[32m    <SccAuxPath>[m
[32m+[m[32m    </SccAuxPath>[m
[32m+[m[32m    <SccProvider>[m
[32m+[m[32m    </SccProvider>[m
   </PropertyGroup>[m
   <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|x86' ">[m
     <PlatformTarget>x86</PlatformTarget>[m
[36m@@ -2996,7 +3000,9 @@[m
       <DependentUpon>Resources.resx</DependentUpon>[m
       <DesignTime>True</DesignTime>[m
     </Compile>[m
[31m-    <None Include="app.config" />[m
[32m+[m[32m    <None Include="app.config">[m
[32m+[m[32m      <SubType>Designer</SubType>[m
[32m+[m[32m    </None>[m
     <None Include="Cashlinx.build" />[m
     <None Include="Properties\Settings.settings">[m
       <Generator>SettingsSingleFileGenerator</Generator>[m
[36m@@ -3460,4 +3466,4 @@[m [mexit 0</PostBuildEvent>[m
   <Target Name="AfterBuild">[m
   </Target>[m
   -->[m
[31m-</Project>[m
[32m+[m[32m</Project>[m
\ No newline at end of file[m
