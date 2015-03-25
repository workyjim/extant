/**
 * OneSimpleTablePaging
 * - A small piece of JS code which does simple table pagination.
 *	 It is based on Ryan Zielke's tablePagination (http://neoalchemy.org/tablePagination.html)
 *   which is licensed under the MIT licenses: http://www.opensource.org/licenses/mit-license.php
 *   Button designs are based on Google+ Buttons in CSS design from Pixify
 *   (http://pixify.com/blog/use-google-plus-to-improve-your-ui/).
 *
 * @author Chun Lin (GCL Project)
 *
 *
 * @name oneSimpleTablePagination
 * @type jQuery
 * @param Object userConfigurations:
 *      rowsPerPage - Number - used to determine the starting rows per page. Default: 10
 *      topNav - Boolean - This specifies the desire to have the navigation be a top nav bar
 *
 *
 * @requires jQuery v1.2.3 or above
 */


$.prototype.extend(
	{
	    'oneSimpleTablePagination': function (userConfigurations) {
	        var defaults = {
	            rowsPerPage: 10,
	            topNav: false,
	            hiddenClass: ''
	        };
	        defaults = $.extend(defaults, userConfigurations);

	        return this.each(function () {
	            var table = $(this)[0];

	            var currPageId = '#tablePagination_currPage';

	            var tblLocation = (defaults.topNav) ? "prev" : "next";

	            var tableRows = $.makeArray($('tr', table));

	            var totalPages = countNumberOfPages(tableRows.length);

	            var currPageNumber = 1;

	            function resetTable(page) {
	                currPageNumber = page;
	                if (defaults.hiddenClass.length > 0) {
	                    tableRows = $.makeArray($('tr:not([class*=' + defaults.hiddenClass + '])', table));
	                } else {
	                    tableRows = $.makeArray($('tr', table));
	                }
	                totalPages = countNumberOfPages(tableRows.length);
	                $('#tablePagination_totalPages').text(totalPages);
	                resetCurrentPage(currPageNumber);
	            }

	            function hideOtherPages(pageNum) {
	                var intRegex = /^\d+$/;
	                if (!intRegex.test(pageNum) || pageNum < 1 || pageNum > totalPages)
	                    return;
	                var startIndex = (pageNum - 1) * defaults.rowsPerPage;
	                var endIndex = (startIndex + defaults.rowsPerPage - 1);
	                $(tableRows).css('display','');
	                for (var i = 0; i < tableRows.length; i++) {
	                    if (i < startIndex || i > endIndex) {
	                        $(tableRows[i]).hide();
	                    }
	                }
	            }

	            function countNumberOfPages(numRows) {
	                var preTotalPages = Math.round(numRows / defaults.rowsPerPage);
	                var totalPages = (preTotalPages * defaults.rowsPerPage < numRows) ? preTotalPages + 1 : preTotalPages;
	                return totalPages;
	            }

	            function resetCurrentPage(currPageNum) {
	                var intRegex = /^\d+$/;
	                if (!intRegex.test(currPageNum) || currPageNum < 1 || currPageNum > totalPages){
	                    $(table)[tblLocation]().find(currPageId).text(totalPages);
                        return;
	                }
                    currPageNumber = currPageNum;
	                hideOtherPages(currPageNumber);
	                $(table)[tblLocation]().find(currPageId).text(currPageNumber);
	            }

	            function createPaginationElements() {
	                var paginationHTML = "";
	                paginationHTML += "<div id='tablePagination' style='text-align: center; border-top: solid 1px gray; padding-top: 5px; padding-bottom: 5px;'>";
	                paginationHTML += "<span id='tablePagination_firstPage' title='First' class='link' style='margin: 2px 5px'>|&lt;</span>";
	                paginationHTML += "<span id='tablePagination_prevPage' title='Previous' class='link' style='margin: 2px 5px'>&lt;&lt;</span>";
	                paginationHTML += "Page <span id='tablePagination_currPage'>" + currPageNumber + "</span> of <span id='tablePagination_totalPages'>" + totalPages + '</span>';
	                paginationHTML += "<span id='tablePagination_nextPage' title='Next' class='link' style='margin: 2px 5px'>&gt;&gt;</span>";
	                paginationHTML += "<span id='tablePagination_lastPage' title='Last' class='link' style='margin: 2px 5px'>&gt;|</span>";
	                paginationHTML += "<span id='tablePagination_reset' style='display:none'></span>";
	                paginationHTML += "<span id='tablePagination_reset_currentpage' style='display:none'></span>";
	                paginationHTML += "</div>";
	                return paginationHTML;
	            }

	            if (defaults.topNav) {
	                $(this).before(createPaginationElements());
	            } else {
	                $(this).after(createPaginationElements());
	            }

	            hideOtherPages(currPageNumber);

	            $(table)[tblLocation]().find('#tablePagination_firstPage').click(function (e) {
	                resetCurrentPage(1);
	            });

	            $(table)[tblLocation]().find('#tablePagination_prevPage').click(function (e) {
	                resetCurrentPage(parseInt(currPageNumber) - 1);
	            });

	            $(table)[tblLocation]().find('#tablePagination_nextPage').click(function (e) {
	                resetCurrentPage(parseInt(currPageNumber) + 1);
	            });

	            $(table)[tblLocation]().find('#tablePagination_lastPage').click(function (e) {
	                resetCurrentPage(totalPages);
	            });

	            $(table)[tblLocation]().find('#tablePagination_reset').click(function (e) {
	                resetTable(1);
	            });

	            $(table)[tblLocation]().find('#tablePagination_reset_currentpage').click(function (e) {
	                resetTable(currPageNumber);
	            });

	        })
	    }
	})