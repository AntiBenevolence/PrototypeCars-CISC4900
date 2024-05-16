import { supabase } from './supabaseClient.js'; // Import Supabase client

// Add event listener to the form submission
document.getElementById('create-post-form').addEventListener('submit', async function(event) {
    event.preventDefault(); // Prevent default form submission

    // Get values from form inputs
    const title = document.getElementById('post-title').value;
    const content = document.getElementById('post-content').value;
    const imageUrl = document.getElementById('post-image-url').value; 

    // Create a post object with title, content, and initial upvote count
    const post = {
        title: title,
        content: content,
        upvote: 0
    };

    // If an image URL is provided, add it to the post object
    if (imageUrl) {
        post.image_url = imageUrl;
    }

    // Insert the post into the 'Posts' table in Supabase
    const { data, error } = await supabase
        .from('Posts')
        .insert([post]);

    // Handle any errors during post creation
    if (error) {
        alert('Error creating post: ' + error.message);
    } else {
        alert('Post created successfully!');
        window.location.href = 'nohesihub.html'; // Redirect to NoHesiHub page
    }
});
