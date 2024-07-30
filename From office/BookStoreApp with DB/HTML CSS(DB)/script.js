$(document).ready(function() {
    let currentPage = 1;
    const pageSize = 10;

    // Function to display books in the table
    function displayBooks(books) {
        console.log('Displaying books:', books);
        $('tbody').empty();
        if (books.length === 0) {
            $('tbody').append('<tr><td colspan="6">No books found</td></tr>');
        } else {
            books.forEach(function(book) {
                $('tbody').append(
                    `<tr>
                        <td>${book.title}</td>
                        <td>${book.author}</td>
                        <td>${book.description}</td>
                        <td>${book.price}</td>
                        <td>${book.availability}</td>
                    </tr>`
                );
            });
        }
    }

    // Function to fetch books with optional search query and pagination
    function fetchBooks(query = '', page = 1, pageSize = 10) {
        console.log('Fetching books with query:', query, 'page:', page, 'pageSize:', pageSize);
        $.ajax({
            url: `https://localhost:7166/api/Books?title=${query}&page=${page}&pageSize=${pageSize}`,
            type: 'GET',
            dataType: 'json',
            success: function(data) {
                console.log('Books data received:', data);
                displayBooks(data.books);
            },
            error: function(jqXHR, textStatus, errorThrown) {
                console.error('Error fetching books:', textStatus, errorThrown);
            }
        });
    }

    // Event handler for search bar input
    $('#search-bar').on('input', function() {
        var query = $(this).val();
        currentPage = 1;
        fetchBooks(query, currentPage);
    });

    // Event handler for load more button
    $('#load-more-btn').click(function() {
        currentPage++;
        var query = $('#search-bar').val();
        fetchBooks(query, currentPage);
    });

    // Event handler for adding a new book
    $('#add-book-form').on('submit', function(e) {
        e.preventDefault();

        var newBook = {
            title: $('#title').val(),
            author: $('#author').val(),
            description: $('#description').val(),
            imageUrl: $('#imageUrl').val(),
            price: parseFloat($('#price').val()), // Ensure price is parsed as a float
            availability: $('#availability').val()
        };

        $.ajax({
            url: 'https://localhost:7166/api/Books',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(newBook),
            success: function() {
                $('#add-book-form')[0].reset();
                currentPage = 1;
                fetchBooks($('#search-bar').val(), currentPage);
                updateFeaturedBooks(); // Update featured books section after adding new book
            },
            error: function(jqXHR, textStatus, errorThrown) {
                console.error('Error adding book:', textStatus, errorThrown);
                // Optional: Display a message to the user
            }
        });
    });

    // Event handler for contact form submission
    $('#contact-form').on('submit', function(e) {
        e.preventDefault();

        var formData = {
            name: $('#name').val(),
            email: $('#email').val(),
            subject: $('#subject').val(),
            message: $('#message').val()
        };

        $.ajax({
            url: 'https://localhost:7166/api/Comments', // Ensure this endpoint matches your CommentsController
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function() {
                $('#contact-form')[0].reset();
                fetchComments(); // Refresh comments after adding a new one
                alert('Message sent successfully!');
            },
            error: function(jqXHR, textStatus, errorThrown) {
                console.error('Error sending message:', textStatus, errorThrown);
                alert('Failed to send message. Please try again later.');
            }
        });
    });


// Function to generate star ratings
function generateStars(rating) {
    let stars = '';
    for (let i = 0; i < 5; i++) {
        if (i < rating) {
            stars += '<span class="star">&#9733;</span>'; // Full star
        } else {
            stars += '<span class="star">&#9734;</span>'; // Empty star
        }
    }
    return stars;
}

function highlightStoreName(text) {
    // Highlight all instances of the store name
    return text.replace(/My Online Book Store/g, '<span class="store-name highlight">My Online Book Store</span>');
}

function fetchComments() {
    $.ajax({
        url: 'https://localhost:7166/api/Comments', // Ensure this URL matches your API endpoint
        type: 'GET',
        dataType: 'json',
        success: function(data) {
            console.log('Comments data received:', data);
            $('#dynamic-reviews').empty(); // Clear existing reviews
            
            if (data.length === 0) {
                $('#dynamic-reviews').append('<p>No comments available.</p>');
            } else {
                data.forEach(function(comment) {
                    // Log the comment before highlighting
                    console.log('Original comment:', comment.message);

                    const highlightedMessage = highlightStoreName(comment.message);

                    // Log the highlighted message to ensure it's being processed
                    console.log('Highlighted comment:', highlightedMessage);

                    $('#dynamic-reviews').append(`
                        <div class="comment-container">
                            <div class="rating">${generateStars(comment.rating)}</div>
                            <p class="comment-text">${highlightedMessage}</p>
                            <p><strong>${comment.name}</strong>: <a href="mailto:${comment.email}">Customer's Profile</a></p>
                        </div>
                    `);
                });
            }
        },
        error: function(jqXHR, textStatus, errorThrown) {
            console.error('Error fetching comments:', textStatus, errorThrown);
        }
    });
}
// Initial fetch on page load
$(document).ready(function() {
    fetchComments();
});

    // Function to update featured books section
    function updateFeaturedBooks() {
        $.ajax({
            url: 'https://localhost:7166/api/Books/featured-books', // Ensure endpoint matches the controller
            method: 'GET',
            success: function(data) {
                console.log('Featured books data received:', data);
                data.forEach((book, index) => {
                    const featuredBook = $(`#featured-book-${index + 1}`);
                    if (featuredBook.length) {
                        featuredBook.find('.featured-image').attr('src', book.imageUrl);
                        featuredBook.find('.book-title').text(book.title);
                        featuredBook.find('.book-author').text(book.author);
                        featuredBook.find('.book-price').text(book.price);
                    }
                });
            },
            error: function(xhr, status, error) {
                console.error('Failed to fetch featured books:', error);
            }
        });
    }

    // Initial fetch on page load
    fetchBooks('', currentPage);
    fetchComments(); // Fetch and display comments on page load
    updateFeaturedBooks(); // Fetch and display featured books on page load

    // Function to handle section scrolling
    $('#section-dropdown').on('change', function() {
        var selectedValue = $(this).val();
        var targetSection = $('#' + selectedValue);

        if (targetSection.length) {
            targetSection[0].scrollIntoView({ behavior: 'smooth' });
        }
    });
});
