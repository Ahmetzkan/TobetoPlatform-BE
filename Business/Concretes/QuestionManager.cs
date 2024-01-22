﻿using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.QuestionRequests;
using Business.Dtos.Responses.QuestionResponses;
using Business.Rules;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class QuestionManager : IQuestionService
    {
        IQuestionDal _questionDal;
        IMapper _mapper;
        QuestionBusinessRules _questionBusinessRules;

        public QuestionManager(IQuestionDal questionDal, IMapper mapper, QuestionBusinessRules questionBusinessRules)
        {
            _questionDal = questionDal;
            _mapper = mapper;
            _questionBusinessRules = questionBusinessRules;
        }
        public async Task<CreatedQuestionResponse> AddAsync(CreateQuestionRequest createQuestionRequest)
        {
            Question question = _mapper.Map<Question>(createQuestionRequest);
            Question addedQuestion = await _questionDal.AddAsync(question);
            CreatedQuestionResponse createdQuestionResponse = _mapper.Map<CreatedQuestionResponse>(addedQuestion);
            return createdQuestionResponse;

        }

        public async Task<DeletedQuestionResponse> DeleteAsync(DeleteQuestionRequest deleteQuestionRequest)
        {
            await _questionBusinessRules.IsExistsQuestion(deleteQuestionRequest.Id);
            Question question = await _questionDal.GetAsync(predicate: a => a.Id == deleteQuestionRequest.Id);
            Question deletedQuestion = await _questionDal.DeleteAsync(question, false);
            DeletedQuestionResponse createdQuestionResponse = _mapper.Map<DeletedQuestionResponse>(deletedQuestion);
            return createdQuestionResponse;
        }

        public async Task<IPaginate<GetListQuestionResponse>> GetListAsync(PageRequest pageRequest)
        {
            var questions = await _questionDal.GetListAsync(
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize
                );
            var mappedQuestions = _mapper.Map<Paginate<GetListQuestionResponse>>(questions);
            return mappedQuestions;
        }

        public async Task<GetListQuestionResponse> GetByIdAsync(Guid id)
        {
            var question = await _questionDal.GetAsync(q => q.Id == id);
            var mappedQuestion = _mapper.Map<GetListQuestionResponse>(question);
            return mappedQuestion;
        }

        public async Task<UpdatedQuestionResponse> UpdateAsync(UpdateQuestionRequest updateQuestionRequest)
        {
            await _questionBusinessRules.IsExistsQuestion(updateQuestionRequest.Id);
            Question question = _mapper.Map<Question>(updateQuestionRequest);
            Question updatedQuestion = await _questionDal.UpdateAsync(question);
            UpdatedQuestionResponse updatedQuestionResponse = _mapper.Map<UpdatedQuestionResponse>(updatedQuestion);
            return updatedQuestionResponse;
        }

        public async Task<IPaginate<GetListQuestionResponse>> GetByExamIdAsync(Guid examId)
        {
            var questionsList = await _questionDal.GetListAsync(
                include: q => q.Include(e => e.ExamQuestions).ThenInclude(eq => eq.Exam));
            var filteredQuestionList = questionsList.Items.Where(e => e.ExamQuestions.Any(s => s.ExamId == examId)).ToList();

            var mappedQuestions = _mapper.Map<Paginate<GetListQuestionResponse>>(filteredQuestionList);
            return mappedQuestions;
        }
    }
}
