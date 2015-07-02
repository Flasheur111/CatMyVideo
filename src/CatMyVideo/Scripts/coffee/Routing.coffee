class Routing
    #Replace every id in the wanted URL
    @_path: (path, params = {}) ->
        for name, value of params
            path = path.replace "{#{name}}", value
        path

    #Possible calls
    @_routes:
        'list_users': (params) =>
            $.ajax @_path("/users", params),
                data: params
                type: 'GET'

        'user_profile': (params) =>
            $.ajax @_path("/users/{userid}", params),
                data: params
                type: 'GET'

        'user_videos': (params) =>
            $.ajax @_path("/users/{userid}/videos", params),
                data: params
                type: 'GET'

        'user_comments': (params) =>
            $.ajax @_path("/users/{userid}/comments", params),
                data: params
                type: 'GET'

        'list_videos': (params) =>
            $.ajax @_path("/videos", params),
                data: params
                type: 'GET'

        'video_info': (params) =>
            $.ajax @_path("/videos/{videoid}", params),
                data: params
                type: 'GET'

        'video_comments': (params) =>
            $.ajax @_path("/videos/{videoid}/comments", params),
                data: params
                type: 'GET'

        'list_comments': (params) =>
            $.ajax @_path("/comments", params),
                data: params
                type: 'GET'

        'create_account': (params) =>
            $.ajax @_path("/users", params),
                data: params
                type: 'POST'

        'add_video': (params) =>
            $.ajax @_path("/users/{userid}/videos", params),
                data: params
                type: 'POST'

        'add_comment': (params) =>
            $.ajax @_path("/videos/{videoid}/comments", params),
                data: params
                type: 'POST'

        'update_info': (params) =>
            $.ajax @_path("/users/{userid}", params),
                data: params
                type: 'PUT'

        'update_video': (params) =>
            $.ajax @_path("/videos/{videoid}", params),
                data: params
                type: 'PUT'

        'update_comment': (params) =>
            $.ajax @_path("/comments/{commentid}", params),
                data: params
                type: 'PUT'

        'change_user_role': (params) =>
            $.ajax @_path("/api/AdminAPI/Role/{userid}", params),
                data: JSON.stringify(params)
                type: 'POST'

        'delete_user': (params) =>
            $.ajax @_path("/users/{userid}", params),
                data: params
                type: 'DELETE'

        'delete_video': (params) =>
            $.ajax @_path("/videos/{videoid}", params),
                data: params
                type: 'DELETE'

        'delete_comment': (params) =>
            $.ajax @_path("/comments/{commentid}", params),
                data: params
                type: 'DELETE'

    #Execute the request
    @_handle: (xhr) =>
        xhr.done((response, txt, xhr) =>
            if response.error
                console.error "Error"
            else
                console.log "Success"
        ).fail((xhr) =>
            try
                return #Useless now but we might want to have a specific handle of error
            catch e
                console.error e
        )

    @on: (route, rest...) =>
        if callback = @_routes[route]
            @_handle callback.apply(@, JSON.stringify(rest))
        else
            console.log "Invalid route called"

window.Routing = Routing