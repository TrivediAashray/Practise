select * from users

select * from UserRoles

select * from Roles

select * from Employees


select U.Name[User's Name],R.Name[Role Name],U.IsActive from Users U left join UserRoles UR on U.Id=UR.UserId left join Roles R on UR.RoleId = R.Id order by UR.Id
