<%@ Application Language="VB" %>
<%@ Import Namespace="System.Web.Routing" %>

<script RunAt="server">
    
    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        RegisterRoutes(RouteTable.Routes)
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.MapPageRoute("","admin","~/Admin.aspx")
        routes.MapPageRoute("","skill","~/Admin2.aspx")
        routes.MapPageRoute("","login","~/Login.aspx")
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
    End Sub
       
</script>
