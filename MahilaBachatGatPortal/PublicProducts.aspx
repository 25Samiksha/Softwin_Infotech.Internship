<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeFile="PublicProducts.aspx.cs"
    Inherits="PublicProducts"
    MasterPageFile="~/Public.Master" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">

    <link href="https://fonts.googleapis.com/css2?family=Outfit:wght@400;500;600;700;800&family=Inter:wght@400;500;600&display=swap"
        rel="stylesheet" />

    <style>
        .products-page {
            width: 100% !important;
            max-width: none !important;
            margin: 0 !important;
            padding: 0 0 80px 0 !important;
            background: #f7f4ee;
            color: #626b64;
            font-family: "Inter", Arial, sans-serif;
        }

        .products-page *,
        .products-page *:before,
        .products-page *:after {
            box-sizing: border-box;
        }

        .products-page > section > .container {
            width: 100% !important;
            max-width: 1200px !important;
            margin-left: auto !important;
            margin-right: auto !important;
            padding-left: 20px !important;
            padding-right: 20px !important;
        }

        .products-page h1,
        .products-page h2,
        .products-page h3,
        .products-page h4 {
            font-family: "Outfit", Arial, sans-serif;
        }

        .products-header {
            position: relative;
            width: 100% !important;
            max-width: none !important;
            min-height: 390px;
            background-image:
                linear-gradient(
                    100deg,
                    rgba(10,15,20,.92) 0%,
                    rgba(10,15,20,.82) 55%,
                    rgba(10,15,20,.62) 100%
                ),
                url("https://images.unsplash.com/photo-1748944080268-0968963f250f?w=1800&h=700&fit=crop&auto=format&q=80");
            background-size: cover;
            background-position: center;
            padding: 85px 0 120px;
            overflow: hidden;
        }

        .products-header:before {
            content: "";
            position: absolute;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background: rgba(0,0,0,.18);
            z-index: 1;
        }

        .products-header-content {
            position: relative;
            z-index: 5;
            width: 100%;
            max-width: 760px;
            margin-left: auto;
            margin-right: auto;
            text-align: center;
            padding: 0 20px;
        }

        .products-label {
            position: relative;
            z-index: 5;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            gap: 8px;
            background: rgba(0,0,0,.35);
            color: #ffffff !important;
            border: 1px solid rgba(255,255,255,.35);
            padding: 8px 18px;
            border-radius: 30px;
            font-size: 12px;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 1.2px;
            margin-bottom: 18px;
        }

        .products-label i {
            color: #ffffff !important;
        }

        .products-title {
            position: relative;
            z-index: 5;
            color: #ffffff !important;
            font-size: 44px;
            font-weight: 700;
            letter-spacing: -1px;
            margin: 0 0 14px 0;
            line-height: 1.2;
            text-shadow: 0 3px 12px rgba(0,0,0,.55);
        }

        .products-subtitle {
            position: relative;
            z-index: 5;
            color: #ffffff !important;
            font-size: 17px;
            line-height: 1.6;
            margin: 0;
            text-shadow: 0 2px 8px rgba(0,0,0,.55);
        }

        .filter-section {
            width: 100% !important;
            margin-top: -55px;
            position: relative;
            z-index: 10;
        }

        .filter-panel {
            width: 100%;
            background: #fffdf9;
            border-radius: 18px;
            padding: 30px;
            border: 1px solid #ddd6ca;
            box-shadow: 0 16px 40px rgba(67,61,53,.13);
        }

        .filter-title {
            display: flex;
            align-items: center;
            gap: 10px;
            color: #3f4d43;
            font-family: "Outfit", Arial, sans-serif;
            font-size: 18px;
            font-weight: 600;
            margin: 0 0 23px 0;
        }

        .filter-title i {
            width: 34px;
            height: 34px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            background: #f0ddd2;
            color: #c97855;
            border-radius: 50%;
            font-size: 13px;
        }

        .filter-label {
            display: block;
            color: #69736c;
            font-size: 11px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: .7px;
            margin-bottom: 8px;
        }

        .filter-control {
            width: 100%;
            height: 45px;
            border: 1px solid #d8d2c8;
            border-radius: 9px;
            padding: 9px 12px;
            color: #4f5b53;
            background: #ffffff;
            box-shadow: none;
            font-size: 13px;
            transition: all .2s ease;
        }

        .filter-control:hover {
            border-color: #c9bfb1;
        }

        .filter-control:focus {
            border-color: #c97855;
            box-shadow: 0 0 0 3px rgba(201,120,85,.13);
            outline: none;
            background: #ffffff;
        }

        .search-button {
            width: 100%;
            height: 45px;
            border: 0;
            border-radius: 9px;
            background: #c97855;
            color: #ffffff;
            font-weight: 700;
            font-size: 13px;
            transition: all .2s ease;
            cursor: pointer;
        }

        .search-button:hover {
            background: #ad6042;
        }

        .clear-button {
            width: 100%;
            height: 39px;
            margin-top: 8px;
            border: 1px solid #d8d2c8;
            border-radius: 9px;
            background: #ffffff;
            color: #69736c;
            font-weight: 600;
            font-size: 12px;
            transition: all .2s ease;
            cursor: pointer;
        }

        .clear-button:hover {
            border-color: #c97855;
            color: #ad6042;
            background: #fff8f4;
        }

        .product-section {
            width: 100% !important;
            padding-top: 65px;
        }

        .product-heading-row {
            display: flex;
            align-items: flex-end;
            justify-content: space-between;
            margin-bottom: 30px;
        }

        .product-heading {
            color: #3f4d43;
            font-size: 30px;
            font-weight: 700;
            margin: 0;
            letter-spacing: -.4px;
        }

        .product-heading-line {
            width: 50px;
            height: 3px;
            background: #c97855;
            border-radius: 5px;
            margin-top: 10px;
        }

        .product-count {
            color: #788179;
            font-size: 12px;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: .8px;
        }

        .product-count i {
            color: #789b7d;
            margin-right: 5px;
        }

        .product-message {
            display: block;
            text-align: center;
            color: #6e766f;
            font-size: 14px;
            background: #f4e4dc;
            border: 1px solid #e5cfc3;
            border-radius: 11px;
            padding: 16px;
            margin-bottom: 25px;
        }

        .product-card {
            background: #ffffff;
            border: 1px solid #ddd8cf;
            border-radius: 16px;
            overflow: hidden;
            height: 100%;
            box-shadow: 0 5px 18px rgba(67,61,53,.07);
            transition: transform .25s ease, box-shadow .25s ease, border-color .25s ease;
        }

        .product-card:hover {
            transform: translateY(-6px);
            box-shadow: 0 18px 38px rgba(67,61,53,.15);
            border-color: #cfc5b8;
        }

        .product-image-container {
            height: 245px;
            background: #eee8dc;
            position: relative;
            overflow: hidden;
        }

        .product-image {
            width: 100% !important;
            height: 100% !important;
            object-fit: cover;
            display: block;
            transition: transform .45s ease;
        }

        .product-card:hover .product-image {
            transform: scale(1.05);
        }

        .product-category-badge {
            position: absolute;
            top: 14px;
            left: 14px;
            background: #ffffff;
            color: #68756b;
            padding: 7px 13px;
            border-radius: 20px;
            font-size: 10px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: .5px;
            box-shadow: 0 4px 12px rgba(67,61,53,.12);
        }

        .product-content {
            padding: 21px;
        }

        .product-name {
            color: #3f4d43;
            font-family: "Outfit", Arial, sans-serif;
            font-size: 19px;
            font-weight: 650;
            line-height: 1.35;
            margin-bottom: 9px;
        }

        .product-gat {
            color: #808981;
            font-size: 12px;
            line-height: 1.5;
            margin-bottom: 11px;
        }

        .product-gat i {
            color: #789b7d;
            margin-right: 3px;
        }

        .product-gat strong {
            color: #59645c;
        }

        .product-description {
            color: #858d87;
            font-size: 12.5px;
            line-height: 1.65;
            margin-bottom: 14px;
            min-height: 41px;
        }

        .product-stock {
            color: #707a73;
            font-size: 12px;
            margin-bottom: 17px;
        }

        .product-stock i {
            color: #789b7d;
            margin-right: 5px;
        }

        .product-bottom {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 12px;
            padding-top: 16px;
            border-top: 1px solid #e8e3db;
        }

        .product-price {
            color: #3f4d43;
            font-family: "Outfit", Arial, sans-serif;
            font-size: 21px;
            font-weight: 700;
            white-space: nowrap;
        }

        .view-button {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            gap: 6px;
            padding: 9px 15px;
            background: #c0395b;
            color: #ffffff !important;
            border: 1px solid #c0395b;
            border-radius: 8px;
            text-decoration: none !important;
            font-size: 12px;
            font-weight: 700;
            transition: all .2s ease;
            white-space: nowrap;
        }

        .view-button:hover {
            background: #9e2d4a;
            border-color: #9e2d4a;
            color: #ffffff !important;
            text-decoration: none !important;
        }

        .view-button:focus {
            background: #9e2d4a;
            border-color: #9e2d4a;
            color: #ffffff !important;
            text-decoration: none !important;
        }

        .view-button i {
            font-size: 10px;
        }

        @media (max-width: 1199px) {
            .products-page > section > .container {
                padding-left: 30px !important;
                padding-right: 30px !important;
            }

            .products-title {
                font-size: 40px;
            }
        }

        @media (max-width: 991px) {
            .products-title {
                font-size: 35px;
            }

            .filter-column {
                margin-bottom: 16px;
            }

            .product-image-container {
                height: 220px;
            }
        }

        @media (max-width: 767px) {
            .products-page > section > .container {
                padding-left: 15px !important;
                padding-right: 15px !important;
            }

            .products-header {
                min-height: 340px;
                padding: 60px 0 90px;
            }

            .products-header-content {
                padding: 0 10px;
            }

            .products-title {
                font-size: 29px;
                letter-spacing: -.5px;
            }

            .products-subtitle {
                font-size: 14px;
                line-height: 1.6;
            }

            .filter-section {
                margin-top: 22px;
            }

            .filter-panel {
                padding: 22px 18px;
            }

            .product-section {
                padding-top: 42px;
            }

            .product-heading-row {
                display: block;
                margin-bottom: 24px;
            }

            .product-heading {
                font-size: 27px;
                margin-bottom: 10px;
            }

            .product-count {
                display: block;
            }

            .product-image-container {
                height: 225px;
            }

            .product-bottom {
                flex-direction: column;
                align-items: stretch;
            }

            .product-price {
                text-align: center;
            }

            .view-button {
                width: 100%;
            }
        }
    </style>

</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="products-page">

        <section class="products-header">
            <div class="container">

                <div class="products-header-content">

                    <div class="products-label">
                        <i class="glyphicon glyphicon-shopping-cart"></i>
                        Mahila Marketplace
                    </div>

                    <h1 class="products-title">
                        Explore Our Products
                    </h1>

                    <p class="products-subtitle">
                        Discover products created and sold by Mahila Bachat Gats
                    </p>

                </div>

            </div>
        </section>

        <section class="filter-section">
            <div class="container">

                <div class="filter-panel">

                    <div class="filter-title">
                        <i class="glyphicon glyphicon-filter"></i>
                        Find What You're Looking For
                    </div>

                    <div class="row">

                        <div class="col-md-4 filter-column">

                            <label class="filter-label">
                                Search Product
                            </label>

                            <asp:TextBox
                                ID="txtSearch"
                                runat="server"
                                CssClass="filter-control"
                                placeholder="Search products...">
                            </asp:TextBox>

                        </div>

                        <div class="col-md-3 filter-column">

                            <label class="filter-label">
                                Category
                            </label>

                            <asp:DropDownList
                                ID="ddlCategory"
                                runat="server"
                                CssClass="filter-control"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                            </asp:DropDownList>

                        </div>

                        <div class="col-md-3 filter-column">

                            <label class="filter-label">
                                Bachat Gat
                            </label>

                            <asp:DropDownList
                                ID="ddlBachatGat"
                                runat="server"
                                CssClass="filter-control"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlBachatGat_SelectedIndexChanged">
                            </asp:DropDownList>

                        </div>

                        <div class="col-md-2">

                            <label class="filter-label">
                                &nbsp;
                            </label>

                            <asp:Button
                                ID="btnSearch"
                                runat="server"
                                Text="Search"
                                CssClass="search-button"
                                OnClick="btnSearch_Click" />

                            <asp:Button
                                ID="btnClear"
                                runat="server"
                                Text="Clear"
                                CssClass="clear-button"
                                OnClick="btnClear_Click" />

                        </div>

                    </div>

                </div>

            </div>
        </section>

        <section class="product-section">

            <div class="container">

                <div class="product-heading-row">

                    <div>

                        <h2 class="product-heading">
                            Our Products
                        </h2>

                        <div class="product-heading-line"></div>

                    </div>

                    <div class="product-count">
                        <i class="glyphicon glyphicon-th"></i>
                        Available Products
                    </div>

                </div>

                <asp:Label
                    ID="lblMessage"
                    runat="server"
                    CssClass="product-message">
                </asp:Label>

                <div class="row">

                    <asp:Repeater
                        ID="rptProducts"
                        runat="server">

                        <ItemTemplate>

                            <div class="col-lg-4 col-md-4 col-sm-6"
                                style="margin-bottom:30px;">

                                <div class="product-card">

                                    <div class="product-image-container">

                                        <asp:Image
                                            ID="imgProduct"
                                            runat="server"
                                            ImageUrl='<%# GetImageUrl(Eval("ProductImage")) %>'
                                            CssClass="product-image"
                                            AlternateText='<%# Eval("ProductName") %>' />

                                        <div class="product-category-badge">
                                            <%# Eval("Category") %>
                                        </div>

                                    </div>

                                    <div class="product-content">

                                        <div class="product-name">
                                            <%# Eval("ProductName") %>
                                        </div>

                                        <div class="product-gat">
                                            <i class="glyphicon glyphicon-home"></i>
                                            <strong>Bachat Gat:</strong>
                                            <%# Eval("GatName") %>
                                        </div>

                                        <div class="product-description">
                                            <%# Eval("Description") %>
                                        </div>

                                        <div class="product-stock">
                                            <i class="glyphicon glyphicon-ok-circle"></i>
                                            <%# Eval("Quantity") %>
                                            <%# Eval("Unit") %>
                                            available
                                        </div>

                                        <div class="product-bottom">

                                            <div class="product-price">
                                                ₹ <%# Eval("SellingPrice", "{0:N2}") %>
                                            </div>

                                            <a
                                                href='<%# "ProductDetails.aspx?ProductID=" + Eval("ProductID") %>'
                                                class="view-button">

                                                View Product

                                                <i class="glyphicon glyphicon-arrow-right"></i>

                                            </a>

                                        </div>

                                    </div>

                                </div>

                            </div>

                        </ItemTemplate>

                    </asp:Repeater>

                </div>

            </div>

        </section>

    </div>

</asp:Content>