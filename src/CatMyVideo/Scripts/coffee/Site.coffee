
f = () ->
    Routing.on "list_users"

changeRole = (elt) ->
  [id, role] = elt.attributes.getNamedItem("id").value.split "_"
  Routing.on "change_user_role",
    userid: id
    role: role
    value: elt.checked
  .success (response) ->
    console.info "coucou"

load_more_comment = (elt) ->
  videoid = elt.attributes.getNamedItem("videoid").value
  loaded = parseInt elt.attributes.getNamedItem("loaded").value
  Routing.on "video_comments",
    videoid: videoid
    page: parseInt(loaded / 20)
  .success (response) ->
    comments = JSON.parse response
    for comment in comments
      postdate = new moment(comment.PostDate)
      $("#comment-display").append "
          <div class='row' id='comment_" + comment.Id + "'>
            <div><a href='#' data-reveal-id='trash-comment'><i class='fi-trash'></i></a></div>
            <div class='medium-10 columns medium-centered'>
                <p>" + comment.Message + "</p></div>
                <div class='row'>
                        <div class='medium-10 medium-centered columns text-right'>
                            By <a href='/Account/Display/" + comment.User.Nickname + "'>" + comment.User.Nickname + "</a>, posted on <em>" + postdate.format("DD/MM/YYYY") + " at " + postdate.format("HH:mm") + "</em>
                        </div>
                    </div>
                    <hr />
                </div>"
    loaded += comments.length
    $("#load-more-comment").attr "loaded", loaded
    max = parseInt elt.attributes.getNamedItem("max").value
    if max <= loaded
      $("#load-more-comment").hide()
    return
  return
