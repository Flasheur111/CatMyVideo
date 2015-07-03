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
            <div><a href='#' onclick='change_modal_delete_comment(" + comment.Id + ")' data-reveal-id='trash-comment'><i class='fi-trash'></i></a></div>
            <div class='medium-10 columns medium-centered'>
                <p id='comment-content-" + comment.Id + "'>" + comment.Message + "</p></div>
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

change_modal_delete_comment = (id) ->
  $("#delete-comment").attr("action", "/Comment/Delete/" + id)
  return

change_modal_update_comment = (id) ->
  $("#change-comment").attr("action", "/Comment/Update/" + id)
  content = $("#comment-content-" + id)[0].innerText
  $("#update-comment-content").val(content)
  return