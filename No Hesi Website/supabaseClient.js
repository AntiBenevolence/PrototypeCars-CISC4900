// Import Supabase client from Skypack CDN
import { createClient } from 'https://cdn.skypack.dev/@supabase/supabase-js';

// Supabase URL and anonymous key for accessing the database
const supabaseUrl = 'https://tpwvqeodqsswzaixarnm.supabase.co'; // Replace with your Supabase project URL
const supabaseKey = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InRwd3ZxZW9kcXNzd3phaXhhcm5tIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTUwMTY3NzksImV4cCI6MjAzMDU5Mjc3OX0.jbf_zWIAovuujlaAshVqLTfyq-Al-PX_wOqZRgD7qPs'; // Replace 'your-anon-key' with your actual Supabase anon key

// Create a Supabase client using the provided URL and key
const supabase = createClient(supabaseUrl, supabaseKey);

// Export the Supabase client for use in other modules
export { supabase };

