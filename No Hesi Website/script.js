import { supabase } from './supabaseClient.js'; // Import Supabase client

// Add event listener to execute when the DOM is fully loaded
document.addEventListener('DOMContentLoaded', function() {
    const postsContainer = document.getElementById('posts-container'); // Get the posts container element
    const newestButton = document.getElementById('newest'); // Get the "Newest" button element
    const mostPopularButton = document.getElementById('mostPopular'); // Get the "Most Popular" button element
    const searchInput = document.getElementById('searchInput'); // Get the search input element
    let sortType = 'newest'; // Default sort type

    // Add click event listener to the "Newest" button
    newestButton.addEventListener('click', () => {
        sortType = 'newest'; // Set sort type to "newest"
        fetchPosts(); // Fetch posts based on the selected sort type
    });

    // Add click event listener to the "Most Popular" button
    mostPopularButton.addEventListener('click', () => {
        sortType = 'mostPopular'; // Set sort type to "mostPopular"
        fetchPosts(); // Fetch posts based on the selected sort type
    });

    // Add input event listener to the search input
    searchInput.addEventListener('input', () => {
        fetchPosts(); // Fetch posts based on the search query
    });

    // Function to fetch posts from Supabase
    async function fetchPosts() {
        let query = supabase.from('Posts').select('*'); // Initial query to select all posts

        // Filter posts based on the search query
        if (searchInput.value) {
            query = query.ilike('title', `%${searchInput.value}%`); 
        }

        // Sort posts based on the selected sort type
        if (sortType === 'newest') {
            query = query.order('created_at', { ascending: false });
        } else if (sortType === 'mostPopular') {
            query = query.order('upvote', { ascending: false });
        }

        const { data, error } = await query; // Execute the query and get data

        // Handle any errors during fetching posts
        if (error) {
            console.error('Error fetching posts:', error);
            return;
        }

        displayPosts(data); // Display the fetched posts
    }

    // Function to display posts in the posts container
    function displayPosts(posts) {
        postsContainer.innerHTML = ''; // Clear the posts container
        posts.forEach(post => {
            const postElement = document.createElement('div'); // Create a new div element for each post
            postElement.className = 'post-card'; // Set the class name for styling
            postElement.innerHTML = `
                <div class="post-meta">
                    <span>Posted ${new Date(post.created_at).toDateString()}</span>
                </div>
                <h3 class="post-title">${post.title}</h3>
                <div class="post-meta">
                    <span>${post.upvote} upvotes</span>
                </div>
            `; // Set the inner HTML of the post element
            postElement.onclick = () => {
                window.location.href = `viewPost.html?postId=${post.id}`; // Redirect to the post detail page on click
            };
            postsContainer.appendChild(postElement); // Append the post element to the posts container
        });
    }

    fetchPosts(); // Fetch and display posts on initial load
});
