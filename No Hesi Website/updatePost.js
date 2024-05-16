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

    // Populate the form fields with the fetched post data
    document.getElementById('title').value = post.title;
    document.getElementById('content').value = post.content;
    document.getElementById('image_url').value = post.image_url;

    // Add submit event listener to the update post form
    const form = document.getElementById('update-post-form');
    form.addEventListener('submit', async (event) => {
        event.preventDefault(); // Prevent default form submission

        // Create an object with updated post data
        const updatedPost = {
            title: document.getElementById('title').value,
            content: document.getElementById('content').value,
            image_url: document.getElementById('image_url').value
        };

        // Update the post in Supabase
        const { error } = await supabase
            .from('Posts')
            .update(updatedPost)
            .eq('id', postId);

        // Handle error if updating the post fails
        if (error) {
            console.error('Error updating post:', error);
            alert('Error updating post.');
        } else {
            alert('Post updated successfully!'); // Alert the user if the post is updated successfully
            window.location.href = 'nohesihub.html'; // Redirect to NoHesiHub page after successful update
        }
    });
});
