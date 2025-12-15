using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace incode.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    admin_id = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    autority = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "cinemas",
                columns: table => new
                {
                    cinema_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    chain = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    city = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    district = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    latitude = table.Column<decimal>(type: "decimal(10,7)", nullable: true),
                    longitude = table.Column<decimal>(type: "decimal(10,7)", nullable: true),
                    phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    facilities = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cinemas__5662877858C3321B", x => x.cinema_id);
                });

            migrationBuilder.CreateTable(
                name: "mission",
                columns: table => new
                {
                    mission_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mission_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    mission_detail = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    target_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    target_count = table.Column<int>(type: "int", nullable: false),
                    reward_points = table.Column<int>(type: "int", nullable: false),
                    reward_exp = table.Column<int>(type: "int", nullable: false),
                    reset_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "none"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__mission__B5419AB28922714E", x => x.mission_id);
                });

            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    movie_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    original_title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    director = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    genre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    release_date = table.Column<DateOnly>(type: "date", nullable: true),
                    duration = table.Column<int>(type: "int", nullable: true),
                    rating = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    poster_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    synopsis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trailer_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    imdb_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__movies__83CDF74926567634", x => x.movie_id);
                });

            migrationBuilder.CreateTable(
                name: "point_type_definition",
                columns: table => new
                {
                    point_type_definition_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    display_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__point_ty__E69D541F8B2CA7DC", x => x.point_type_definition_id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    image_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    is_physical = table.Column<bool>(type: "bit", nullable: false),
                    points = table.Column<int>(type: "int", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    is_on_sale = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__product__47027DF5D3AE3345", x => x.product_id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    nickname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user__B9BE370F6AA687F6", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "entrust_mission",
                columns: table => new
                {
                    entrust_mission_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    detail = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    reward_points = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "open"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__entrust___19471E9515C1AB86", x => x.entrust_mission_id);
                    table.ForeignKey(
                        name: "FK__entrust_m__user___32AB8735",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "follows",
                columns: table => new
                {
                    follower_id = table.Column<int>(type: "int", nullable: false),
                    followee_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__follows__710D19E636F1C339", x => new { x.follower_id, x.followee_id });
                    table.ForeignKey(
                        name: "FK__follows__followe__282DF8C2",
                        column: x => x.follower_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__follows__followe__29221CFB",
                        column: x => x.followee_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    image_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    image_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    category = table.Column<int>(type: "int", nullable: false),
                    caption = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    display_order = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__images__DC9AC955DE475D8F", x => x.image_id);
                    table.ForeignKey(
                        name: "FK__images__user_id__2739D489",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "mission_progress",
                columns: table => new
                {
                    mission_progress_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    mission_id = table.Column<int>(type: "int", nullable: false),
                    current_count = table.Column<int>(type: "int", nullable: false),
                    is_completed = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "('0')"),
                    accepted_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    completed_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__mission___F43E603D4DB2CE0E", x => x.mission_progress_id);
                    table.ForeignKey(
                        name: "FK__mission_p__missi__2CF2ADDF",
                        column: x => x.mission_id,
                        principalTable: "mission",
                        principalColumn: "mission_id");
                    table.ForeignKey(
                        name: "FK__mission_p__user___31B762FC",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    total_price = table.Column<int>(type: "int", nullable: false),
                    order_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    requires_shipping = table.Column<bool>(type: "bit", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order__46596229014D95EC", x => x.order_id);
                    table.ForeignKey(
                        name: "FK__order__user_id__3493CFA7",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "point_log",
                columns: table => new
                {
                    point_log_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    source_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    source_id = table.Column<int>(type: "int", nullable: false),
                    point_type_definition_id = table.Column<int>(type: "int", nullable: false),
                    change_amount = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, defaultValue: "unknow"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__point_lo__E2EBFE7C152E2E06", x => x.point_log_id);
                    table.ForeignKey(
                        name: "FK__point_log__point__2EDAF651",
                        column: x => x.point_type_definition_id,
                        principalTable: "point_type_definition",
                        principalColumn: "point_type_definition_id");
                    table.ForeignKey(
                        name: "FK__point_log__user___2BFE89A6",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "point_purchase_log",
                columns: table => new
                {
                    point_purchase_log_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    order_no = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    cash_amount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    points_amount = table.Column<int>(type: "int", nullable: false),
                    payment_method = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "pending"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    completed_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__point_pu__134871228085866B", x => x.point_purchase_log_id);
                    table.ForeignKey(
                        name: "FK__point_pur__user___2DE6D218",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    posts_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    posts_user_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    posts_content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    posts_view_count = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    posts_created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    posts_updat_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsCommission = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__posts__7F3EFABE1C8EE550", x => x.posts_id);
                    table.ForeignKey(
                        name: "FK__posts__posts_use__3864608B",
                        column: x => x.posts_user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "review_helpful_votes",
                columns: table => new
                {
                    vote_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    review_type = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    review_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    is_helpful = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__review_h__9F5405AE9574994C", x => x.vote_id);
                    table.ForeignKey(
                        name: "FK__review_he__user___245D67DE",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "user_login",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: true),
                    logintime = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    loction = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__user_logi__user___2B0A656D",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "user_movie_list",
                columns: table => new
                {
                    list_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    movie_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    watch_date = table.Column<DateOnly>(type: "date", nullable: true),
                    personal_rating = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_mov__7B9EF135F96DF46B", x => x.list_id);
                    table.ForeignKey(
                        name: "FK__user_movi__movie__2645B050",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "movie_id");
                    table.ForeignKey(
                        name: "FK__user_movi__user___25518C17",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "user_status",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: true),
                    experience = table.Column<int>(type: "int", nullable: true),
                    rank = table.Column<int>(type: "int", nullable: true),
                    item_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__user_stat__user___2A164134",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "wallet_transactions",
                columns: table => new
                {
                    wallet_transactions_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    transaction_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "pending"),
                    related_order_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__wallet_t__54E8436663D419C3", x => x.wallet_transactions_id);
                    table.ForeignKey(
                        name: "FK__wallet_tr__user___2FCF1A8A",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "entrust_mission_progress",
                columns: table => new
                {
                    entrust_mission_progress_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    entrust_mission_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "accepted"),
                    is_completed = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "('0')"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__entrust___8F68D7F47425446F", x => x.entrust_mission_progress_id);
                    table.ForeignKey(
                        name: "FK__entrust_m__entru__30C33EC3",
                        column: x => x.entrust_mission_id,
                        principalTable: "entrust_mission",
                        principalColumn: "entrust_mission_id");
                    table.ForeignKey(
                        name: "FK__entrust_m__user___339FAB6E",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "order_detail",
                columns: table => new
                {
                    order_detail_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_price_at_purchase = table.Column<int>(type: "int", nullable: false),
                    is_shipped = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order_de__3C5A4080663AB9A6", x => x.order_detail_id);
                    table.ForeignKey(
                        name: "FK__order_det__order__3587F3E0",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__order_det__produ__367C1819",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateTable(
                name: "cinema_reviews",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cinema_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    seat_rating = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    screen_rating = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    service_rating = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    environment_rating = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    price_rating = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    visit_date = table.Column<DateOnly>(type: "date", nullable: true),
                    movie_watched = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    helpful_count = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    view_count = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    linked_post_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cinema_r__60883D90C38AB1FA", x => x.review_id);
                    table.ForeignKey(
                        name: "FK__cinema_re__cinem__22751F6C",
                        column: x => x.cinema_id,
                        principalTable: "cinemas",
                        principalColumn: "cinema_id");
                    table.ForeignKey(
                        name: "FK__cinema_re__linke__236943A5",
                        column: x => x.linked_post_id,
                        principalTable: "posts",
                        principalColumn: "posts_id");
                    table.ForeignKey(
                        name: "FK__cinema_re__user___2180FB33",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "movie_reviews",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    movie_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    overall_rating = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    story_rating = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    acting_rating = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    visual_rating = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    music_rating = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_spoiler = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    helpful_count = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    view_count = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    linked_post_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__movie_re__60883D9059F97F9B", x => x.review_id);
                    table.ForeignKey(
                        name: "FK__movie_rev__linke__208CD6FA",
                        column: x => x.linked_post_id,
                        principalTable: "posts",
                        principalColumn: "posts_id");
                    table.ForeignKey(
                        name: "FK__movie_rev__movie__1F98B2C1",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "movie_id");
                    table.ForeignKey(
                        name: "FK__movie_rev__user___1EA48E88",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "posts_likes",
                columns: table => new
                {
                    likes_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    posts_likes_id = table.Column<int>(type: "int", nullable: false),
                    likes_user_id = table.Column<int>(type: "int", nullable: false),
                    likes_created_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__posts_li__0CA5EC54D5C208D2", x => x.likes_id);
                    table.ForeignKey(
                        name: "FK__posts_lik__likes__3C34F16F",
                        column: x => x.likes_user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__posts_lik__posts__40058253",
                        column: x => x.posts_likes_id,
                        principalTable: "posts",
                        principalColumn: "posts_id");
                });

            migrationBuilder.CreateTable(
                name: "posts_replies",
                columns: table => new
                {
                    reply_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    replies_post_id = table.Column<int>(type: "int", nullable: false),
                    replies_user_id = table.Column<int>(type: "int", nullable: false),
                    replies_content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    target_user_id = table.Column<int>(type: "int", nullable: true),
                    replies_created_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__posts_re__EE405698AB031DA3", x => x.reply_id);
                    table.ForeignKey(
                        name: "FK__posts_rep__repli__3A4CA8FD",
                        column: x => x.replies_user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__posts_rep__repli__3F115E1A",
                        column: x => x.replies_post_id,
                        principalTable: "posts",
                        principalColumn: "posts_id");
                    table.ForeignKey(
                        name: "FK__posts_rep__targe__3B40CD36",
                        column: x => x.target_user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "save_posts",
                columns: table => new
                {
                    save_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    save_user_id = table.Column<int>(type: "int", nullable: false),
                    save_posts_id = table.Column<int>(type: "int", nullable: false),
                    save_created_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__save_pos__319269DE7F623BE0", x => x.save_id);
                    table.ForeignKey(
                        name: "FK__save_post__save___395884C4",
                        column: x => x.save_user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__save_post__save___3E1D39E1",
                        column: x => x.save_posts_id,
                        principalTable: "posts",
                        principalColumn: "posts_id");
                });

            migrationBuilder.CreateTable(
                name: "shipped_detail",
                columns: table => new
                {
                    shipped_detail_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_detail_id = table.Column<int>(type: "int", nullable: false),
                    recipient_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    recipient_address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    recipient_phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    shipped_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    arrival_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_completed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__shipped___26C93C9342E38DFB", x => x.shipped_detail_id);
                    table.ForeignKey(
                        name: "FK__shipped_d__order__37703C52",
                        column: x => x.order_detail_id,
                        principalTable: "order_detail",
                        principalColumn: "order_detail_id");
                });

            migrationBuilder.CreateTable(
                name: "post_reply_likes",
                columns: table => new
                {
                    reply_likes_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reply_ikes_id = table.Column<int>(type: "int", nullable: false),
                    reply_likes_user_id = table.Column<int>(type: "int", nullable: false),
                    reply_likes_created_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__post_rep__BA01D0E784F6B6CC", x => x.reply_likes_id);
                    table.ForeignKey(
                        name: "FK__post_repl__reply__3D2915A8",
                        column: x => x.reply_likes_user_id,
                        principalTable: "user",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__post_repl__reply__40F9A68C",
                        column: x => x.reply_ikes_id,
                        principalTable: "posts_replies",
                        principalColumn: "reply_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_cinema_reviews_cinema_id",
                table: "cinema_reviews",
                column: "cinema_id");

            migrationBuilder.CreateIndex(
                name: "IX_cinema_reviews_linked_post_id",
                table: "cinema_reviews",
                column: "linked_post_id");

            migrationBuilder.CreateIndex(
                name: "IX_cinema_reviews_user_id",
                table: "cinema_reviews",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "idx_city",
                table: "cinemas",
                column: "city");

            migrationBuilder.CreateIndex(
                name: "idx_location",
                table: "cinemas",
                columns: new[] { "latitude", "longitude" });

            migrationBuilder.CreateIndex(
                name: "IX_entrust_mission_user_id",
                table: "entrust_mission",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "entrust_mission_progress_index_2",
                table: "entrust_mission_progress",
                columns: new[] { "entrust_mission_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_entrust_mission_progress_user_id",
                table: "entrust_mission_progress",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_follows_followee_id",
                table: "follows",
                column: "followee_id");

            migrationBuilder.CreateIndex(
                name: "IX_images_user_id",
                table: "images",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_mission_progress_mission_id",
                table: "mission_progress",
                column: "mission_id");

            migrationBuilder.CreateIndex(
                name: "mission_progress_index_1",
                table: "mission_progress",
                columns: new[] { "user_id", "mission_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_movie",
                table: "movie_reviews",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "idx_rating",
                table: "movie_reviews",
                column: "overall_rating");

            migrationBuilder.CreateIndex(
                name: "idx_user",
                table: "movie_reviews",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_movie_reviews_linked_post_id",
                table: "movie_reviews",
                column: "linked_post_id");

            migrationBuilder.CreateIndex(
                name: "movie_reviews_index_2",
                table: "movie_reviews",
                columns: new[] { "movie_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_user_id",
                table: "order",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_order_id",
                table: "order_detail",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_product_id",
                table: "order_detail",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_point_log_point_type_definition_id",
                table: "point_log",
                column: "point_type_definition_id");

            migrationBuilder.CreateIndex(
                name: "IX_point_log_user_id",
                table: "point_log",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_point_purchase_log_user_id",
                table: "point_purchase_log",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__point_pu__465C81B81B35C2DD",
                table: "point_purchase_log",
                column: "order_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__point_ty__543C4FD940BBCD54",
                table: "point_type_definition",
                column: "type_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_post_reply_likes_reply_ikes_id",
                table: "post_reply_likes",
                column: "reply_ikes_id");

            migrationBuilder.CreateIndex(
                name: "IX_post_reply_likes_reply_likes_user_id",
                table: "post_reply_likes",
                column: "reply_likes_user_id");

            migrationBuilder.CreateIndex(
                name: "post_reply_likes_index_10",
                table: "post_reply_likes",
                columns: new[] { "reply_likes_id", "reply_likes_user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_posts_posts_user_id",
                table: "posts",
                column: "posts_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_posts_likes_likes_user_id",
                table: "posts_likes",
                column: "likes_user_id");

            migrationBuilder.CreateIndex(
                name: "posts_likes_index_9",
                table: "posts_likes",
                columns: new[] { "posts_likes_id", "likes_user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_posts_replies_replies_post_id",
                table: "posts_replies",
                column: "replies_post_id");

            migrationBuilder.CreateIndex(
                name: "IX_posts_replies_replies_user_id",
                table: "posts_replies",
                column: "replies_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_posts_replies_target_user_id",
                table: "posts_replies",
                column: "target_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_review_helpful_votes_user_id",
                table: "review_helpful_votes",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_save_posts_save_posts_id",
                table: "save_posts",
                column: "save_posts_id");

            migrationBuilder.CreateIndex(
                name: "save_posts_index_8",
                table: "save_posts",
                columns: new[] { "save_user_id", "save_posts_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_shipped_detail_order_detail_id",
                table: "shipped_detail",
                column: "order_detail_id");

            migrationBuilder.CreateIndex(
                name: "UQ__user__5CF1C59B3264F725",
                table: "user",
                column: "nickname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_login_user_id",
                table: "user_login",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_movie_list_movie_id",
                table: "user_movie_list",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_movie_list_user_id",
                table: "user_movie_list",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_status_user_id",
                table: "user_status",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_wallet_transactions_user_id",
                table: "wallet_transactions",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "cinema_reviews");

            migrationBuilder.DropTable(
                name: "entrust_mission_progress");

            migrationBuilder.DropTable(
                name: "follows");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "mission_progress");

            migrationBuilder.DropTable(
                name: "movie_reviews");

            migrationBuilder.DropTable(
                name: "point_log");

            migrationBuilder.DropTable(
                name: "point_purchase_log");

            migrationBuilder.DropTable(
                name: "post_reply_likes");

            migrationBuilder.DropTable(
                name: "posts_likes");

            migrationBuilder.DropTable(
                name: "review_helpful_votes");

            migrationBuilder.DropTable(
                name: "save_posts");

            migrationBuilder.DropTable(
                name: "shipped_detail");

            migrationBuilder.DropTable(
                name: "user_login");

            migrationBuilder.DropTable(
                name: "user_movie_list");

            migrationBuilder.DropTable(
                name: "user_status");

            migrationBuilder.DropTable(
                name: "wallet_transactions");

            migrationBuilder.DropTable(
                name: "cinemas");

            migrationBuilder.DropTable(
                name: "entrust_mission");

            migrationBuilder.DropTable(
                name: "mission");

            migrationBuilder.DropTable(
                name: "point_type_definition");

            migrationBuilder.DropTable(
                name: "posts_replies");

            migrationBuilder.DropTable(
                name: "order_detail");

            migrationBuilder.DropTable(
                name: "movies");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
