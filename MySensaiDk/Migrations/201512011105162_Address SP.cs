namespace MySensaiDk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressSP : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Address_Insert",
                p => new
                    {
                        CityId = p.Int(),
                        FullAddress = p.String(maxLength: 100),
                        AddressName = p.String(maxLength: 50),
                        UserId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[Addresses]([CityId], [FullAddress], [AddressName], [UserId])
                      VALUES (@CityId, @FullAddress, @AddressName, @UserId)
                      
                      DECLARE @AddressId int
                      SELECT @AddressId = [AddressId]
                      FROM [dbo].[Addresses]
                      WHERE @@ROWCOUNT > 0 AND [AddressId] = scope_identity()
                      
                      SELECT t0.[AddressId]
                      FROM [dbo].[Addresses] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[AddressId] = @AddressId"
            );
            
            CreateStoredProcedure(
                "dbo.Address_Update",
                p => new
                    {
                        AddressId = p.Int(),
                        CityId = p.Int(),
                        FullAddress = p.String(maxLength: 100),
                        AddressName = p.String(maxLength: 50),
                        UserId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[Addresses]
                      SET [CityId] = @CityId, [FullAddress] = @FullAddress, [AddressName] = @AddressName, [UserId] = @UserId
                      WHERE ([AddressId] = @AddressId)"
            );
            
            // The storede procedure will return all courses that referenced the deleted address, set the references to null, and delete the address
            // TODO: We should prompt the user to provide new addresses to all affected courses
            CreateStoredProcedure(
                "dbo.Address_Delete",
                p => new
                    {
                        AddressId = p.Int(),
                    },
                body:
                    @"SELECT CourseId
		            FROM Courses
		            WHERE @AddressId = @AddressId;

		            UPDATE dbo.Courses
		            SET AddressId = NULL
		            WHERE CourseId = (SELECT CourseId FROM Courses WHERE @AddressId = @AddressId);

		            DELETE [dbo].[Addresses]
                    WHERE ([AddressId] = @AddressId)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Address_Delete");
            DropStoredProcedure("dbo.Address_Update");
            DropStoredProcedure("dbo.Address_Insert");
        }
    }
}
