import { supabase } from './supabaseClient.js'; // Import Supabase client

// Wait for the DOM to be fully loaded before executing the script
document.addEventListener('DOMContentLoaded', async function() {
    const params = new URLSearchParams(window.location.search); // Get URL parameters
    const postId = params.get('postId'); // Get the postId from the URL parameters

    // Check if postId is provided in the URL
    if (!postId) {
        alert('No post ID provided!'); // Alert the user if no postId is found
        return;
    }

    // Fetch the post data from Supabase based on postId
    const { data: post, error } = await supabase
        .from('Posts')
        .select('*')
        .eq('id', postId)
        .single();

    // Handle error if fetching the post fails
    if (error) {
        console.error('Error fetching post:', error);
        return;
    }

    const postContainer = document.getElementById('view-post-container');
    if (!postContainer) {
        console.error('Post container not found!'); // Error if the post container is not found
        return;
    }

    // Populate the post container with the fetched post data
    postContainer.innerHTML = `
        <h1>${post.title}</h1>
        <br>
        <p>${post.content}</p>
        <br>
        ${post.image_url ? `<img src="${post.image_url}" alt="Post Image" style="max-width: 100%; height: auto;">` : ''}
        <p id="upvote-count">${post.upvote} upvotes</p>
        <button id="upvote-button" class="upvote-button">Upvote</button>
        <button id="update-button" class="update-button">✏️</button>
        <button id="delete-button" class="delete-button">🗑️</button>
        <h2>Comments</h2>
        <div id="comments-container" class="comments-container"></div>
        <textarea id="comment-input" class="comment-input" placeholder="Write a comment..."></textarea>
        <button id="add-comment-button" class="comment-button">Add Comment</button>
    `;

    // Get references to the buttons and containers
    const upvoteButton = document.getElementById('upvote-button');
    const updateButton = document.getElementById('update-button');
    const deleteButton = document.getElementById('delete-button');
    const commentsContainer = document.getElementById('comments-container');
    const commentInput = document.getElementById('comment-input');
    const addCommentButton = document.getElementById('add-comment-button');

    // Fetch comments for the post from Supabase
    const { data: comments, error: commentsError } = await supabase
        .from('Comments')
        .select('*')
        .eq('post_id', postId);

    // Handle error if fetching comments fails
    if (commentsError) {
        console.error('Error fetching comments:', commentsError);
    } else {
        // Display each comment in the comments container
        comments.forEach(comment => {
            const commentElement = document.createElement('div');
            commentElement.className = 'comment';
            commentElement.innerText = comment.content;
            commentsContainer.appendChild(commentElement);
        });
    }

    // Add event listener to the upvote button
    upvoteButton.addEventListener('click', async () => {
        const newUpvoteCount = post.upvote + 1; // Increment the upvote count
        const { error } = await supabase
            .from('Posts')
            .update({ upvote: newUpvoteCount })
            .eq('id', postId);

        // Handle error if updating the upvote fails
        if (error) {
            console.error('Error updating upvote:', error);
        } else {
            post.upvote = newUpvoteCount; // Update the upvote count
            document.getElementById('upvote-count').innerText = `${post.upvote} upvotes`;
            alert('Upvoted successfully!'); // Alert the user on successful upvote
        }
    });

    // Add event listener to the update button
    updateButton.addEventListener('click', () => {
        window.location.href = `updatePost.html?postId=${postId}`; // Redirect to the update post page
    });

    // Add event listener to the delete button
    deleteButton.addEventListener('click', async () => {
        const confirmation = confirm('Are you sure you want to delete this post?'); // Confirm deletion

        if (confirmation) {
            const { data, error } = await supabase
                .from('Posts')
                .delete()
                .eq('id', postId)
                .select();

            // Handle error if deleting the post fails
            if (error) {
                console.error('Error deleting post:', error);
                alert('Error deleting post.');
            } else {
                alert('Post deleted successfully!'); // Alert the user on successful deletion
                console.log('Deleted post:', data); 
                window.location.href = 'nohesihub.html'; // Redirect to NoHesiHub page after successful deletion
            }
        }
    });

    // Add event listener to the add comment button
    addCommentButton.addEventListener('click', async () => {
        const commentText = commentInput.value.trim(); // Get the trimmed comment text

        if (commentText) {
            const { data: newComment, error: commentError } = await supabase
                .from('Comments')
                .insert([{ post_id: postId, content: commentText }])
                .select();

            // Handle error if adding the comment fails
            if (commentError) {
                console.error('Error adding comment:', commentError);
                alert('Error adding comment.');
            } else {
                // Display the new comment in the comments container
                const commentElement = document.createElement('div');
                commentElement.className = 'comment';
                commentElement.innerText = newComment[0].content;
                commentsContainer.appendChild(commentElement);
                commentInput.value = ''; // Clear the comment input field
            }
        }
    });
});
