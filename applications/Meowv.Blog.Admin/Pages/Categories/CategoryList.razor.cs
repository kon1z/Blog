﻿using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Admin.Pages.Categories;

public partial class CategoryList
{
    private readonly UpdateCategoryInput input = new();
    private List<GetAdminCategoryDto> categories;

    private string categoryId;

    private bool visible;

    protected override async Task OnInitializedAsync()
    {
        categories = await GetCategoryListAsync();
    }

    public async Task<List<GetAdminCategoryDto>> GetCategoryListAsync()
    {
        var response = await GetResultAsync<BlogResponse<List<GetAdminCategoryDto>>>("api/meowv/blog/admin/categories");
        return response.Result;
    }

    public async Task DeleteAsync(string id)
    {
        var response = await GetResultAsync<BlogResponse>($"api/meowv/blog/category/{id}", method: HttpMethod.Delete);
        if (response.Success)
        {
            await Message.Success("Successful", 0.5);
            categories = await GetCategoryListAsync();
        }
        else
        {
            await Message.Error(response.Message);
        }
    }

    private void Close()
    {
        visible = false;
    }

    private void Open(GetAdminCategoryDto dto)
    {
        categoryId = dto.Id;
        input.Name = dto.Name;
        input.Alias = dto.Alias;

        visible = true;
    }

    public async Task HandleSubmit()
    {
        if (string.IsNullOrWhiteSpace(input.Name) || string.IsNullOrWhiteSpace(input.Alias)) return;

        var json = JsonSerializer.Serialize(input);

        var response =
            await GetResultAsync<BlogResponse>($"api/meowv/blog/category/{categoryId}", json, HttpMethod.Put);
        if (response.Success)
        {
            Close();
            await Message.Success("Successful", 0.5);
            categories = await GetCategoryListAsync();
        }
        else
        {
            await Message.Error(response.Message);
        }
    }
}