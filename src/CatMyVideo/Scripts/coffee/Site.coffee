
f = () ->
    Routing.on "list_users"

changeRole = (elt) ->
  [id, role] = elt.attributes.getNamedItem("id").value.split "_"
  Routing.on "change_user_role",
    userid: id
    role: role
    checked: elt.checked
  .success (response) ->
    console.info "coucou"